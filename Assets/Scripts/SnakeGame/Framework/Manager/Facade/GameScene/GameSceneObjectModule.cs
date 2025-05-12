using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameSceneObjectModule : IModule
{
    private GameState gameState;
    public GameSceneObjectModule(GameState gameState)
    {
        this.gameState = gameState;
    }
    //蛇的碰撞在这里统一管理，未来使用多线程进行计算优化
    // 存放所有蛇的容器
    private List<Snake> snakeList;
    // Remove Buffer
    private Queue<Snake> removeQueue = new Queue<Snake>();

    // 碰撞判定的距离阈值，根据实际情况调整
    public float collisionThreshold = 0.5f;
    public void Initialize()
    {
        snakeList = World.Instance.SnakeList;

        Snake player = Object3DFactory.CreateProduct(Object3DType.SnakePlayer) as Snake;
        SnakeData snakeData = new SnakeData(ConfigManager.Instance.GetSnakeConfig(1) as SnakeConfig);
        player.InitializeData(snakeData);
        player.Create();
        //snakeList.Add(player);


        CreatorEnemySnakeAsync();
        CreatorFoodAsync();
    }

    /// <summary>
    /// 异步创建敌人蛇的方法
    /// </summary>
    async void CreatorEnemySnakeAsync()
    {
        // 循环创建7条初始的敌人蛇
        for (int i = 2; i <= 7; i++)
        {
            // 创建敌人蛇对象
            Snake enemy = Object3DFactory.CreateProduct(Object3DType.SnakeEnemy) as Snake;
            // 初始化敌人蛇的数据
            SnakeData snakeDataEnemy = new SnakeData(ConfigManager.Instance.GetSnakeConfig((uint)i));
            enemy.InitializeData(snakeDataEnemy);
            // 在场景中创建敌人蛇
            enemy.Create();
            // 将敌人蛇添加到蛇列表中
            //snakeList.Add(enemy);
            // 设置敌人蛇的随机位置
            Vector2 v2 = Random.insideUnitCircle * 30;
            enemy.Obj.transform.position = new Vector3(v2.x, 0, v2.y);
        }

        // 获取游戏循环的取消令牌
        var token = GameLoop.Instance.GetCancellationTokenOnDestroy();

        // 当游戏循环未被取消时，持续生成敌人蛇
        while (!token.IsCancellationRequested) // 取消任务时自动退出循环
        {
            // 等待直到敌人蛇的数量少于15
            //  SuppressCancellationThrow() ✅ 避免抛出 OperationCanceledException
            await UniTask.WaitUntil(() => snakeList.Count < 15).AttachExternalCancellation(token).SuppressCancellationThrow();

            // 检查游戏循环是否已被取消
            if (token.IsCancellationRequested)
            {
                Debug.Log("GameLoop 被销毁，停止生成敌人");
                break;
            }

            // 随机选择一个蛇配置ID
            int r = Random.Range(1, 8); // 1-7
            // 获取对应的蛇配置
            SnakeConfig snakeConfig = ConfigManager.Instance.GetSnakeConfig((uint)r);
            // 如果蛇配置为空，输出警告信息并返回
            if (snakeConfig == null)
            {
                Debug.LogWarning($"无法获取 SnakeConfig，ID: {r}");
                return;
            }

            // 创建新的敌人蛇对象
            Snake enemy = Object3DFactory.CreateProduct(Object3DType.SnakeEnemy) as Snake;
            // 如果敌人蛇对象创建失败，输出警告信息并返回
            if (enemy == null)
            {
                Debug.LogWarning("创建 SnakeEnemy 失败");
                return;
            }

            // 初始化敌人蛇的数据，包括蛇配置和玩家的蛇身长度
            SnakeData snakeDataEnemy = new SnakeData(snakeConfig, player.data.bodyLength);
            enemy.InitializeData(snakeDataEnemy);
            // 在场景中创建敌人蛇
            enemy.Create();
            // 将敌人蛇添加到蛇列表中
            snakeList.Add(enemy);

            // 设置敌人蛇的随机位置
            Vector2 v2 = Random.insideUnitCircle * 50;
            enemy.head.transform.position = new Vector3(v2.x, 0, v2.y);
        }
    }


    // 食物容器
    private Dictionary<uint, Food> foodDict = World.Instance.FoodDict;
    // 合理的食物数量
    private const int MIN_FOOD_COUNT = 200;

    /// <summary>
    /// 异步创建食物的方法
    /// </summary>
    async void CreatorFoodAsync()
    {
        // 获取游戏循环的取消令牌
        var token = GameLoop.Instance.GetCancellationTokenOnDestroy();

        while (!token.IsCancellationRequested)
        {
            await UniTask.WaitUntil(() => foodDict.Count < MIN_FOOD_COUNT).AttachExternalCancellation(token).SuppressCancellationThrow();

            if (token.IsCancellationRequested)
            {
                Debug.Log("GameLoop 被销毁，停止生成食物");
                break;
            }

            Food food = Object3DFactory.CreateProduct(Object3DType.Food) as Food;
            // 随机选择一个材质配置ID
            int r = Random.Range(0, snakeList.Count);
            //从蛇身上获取材质
            Material mat = snakeList[r].head.GetComponent<MeshRenderer>().material;
            FoodData foodData = new FoodData(mat);
            food.InitializeData(foodData);
            // 在场景中创建食物
            food.Create();
            // 设置食物缩放
            food.Obj.transform.localScale = Vector3.one * 0.5f;
            // 设置随机位置(原点正负45的矩形区域)
            food.Obj.transform.position = new Vector3(
                Random.Range(-45f, 45f),
                0,
                Random.Range(-45f, 45f)
            );
        }
        // 设置食物的随机位置



    }

    public void Update(float deltaTime)
    {
        CheckCollisions();

        //主角为空跳场景
        if (player.Obj ==null)
        {
            if(GameObject.Find("Crown(Clone)")!=null)
            {   
                GameObject.Destroy(GameObject.Find("Crown(Clone)"));
            }
            gameState.controller.ChangeState(SceneStateEnum.GameOver);
        }
    }

    /// <summary>
    /// 每帧检测所有蛇头与其他蛇碰撞情况
    /// </summary>
    void CheckCollisions()
    {
        // 遍历所有蛇
        foreach (Snake snake in snakeList)
        {
            Vector3 myHeadPos = snake.head.position;

            // 跳过Boss蛇的碰撞检测
            if (snake is SnakeBoss)
                continue;

            // 对比其他蛇的头部和身体
            foreach (Snake otherSnake in snakeList)
            {
                // 排除自身碰撞检测
                if (otherSnake == snake)
                    continue;

                // 跳过与Boss蛇的碰撞检测
                if (otherSnake is SnakeBoss)
                    continue;

                // 检测是否撞击对方的头部
                if (Vector3.Distance(myHeadPos, otherSnake.head.position) < collisionThreshold)
                {
                    Debug.Log($"{snake.Obj.name} 的蛇头撞击了 {otherSnake.Obj.name} 的蛇头");
                    // 此处可加入后续处理逻辑
                    //判断谁的等级低，谁销毁，变成食物
                    Snake lvLow;
                    if (snake.data.lv < otherSnake.data.lv)
                    {
                        lvLow = snake;
                    }
                    else
                    {
                        lvLow = otherSnake;
                    }
                    //身体全部变为食物
                    gameState.CommandModule.AddCommand(new IntoFoodCommand(lvLow));
                    //销毁低等级蛇
                    DestroySnake(lvLow);
                }

                // 检测是否撞击对方的身体
                foreach (Transform bodySegment in otherSnake.list)
                {
                    if (Vector3.Distance(myHeadPos, bodySegment.position) < collisionThreshold)
                    {
                        Debug.Log($"{snake.Obj.name} 的蛇头撞击了 {otherSnake.Obj.name} 的身体段");
                        // 此处可加入后续处理逻辑
                        //先判等级
                        if (snake.data.lv <= otherSnake.data.lv)
                        {
                            //蛇头比对方身体等级低
                            snake.head.position += -snake.head.forward;
                            gameState.CommandModule.AddCommand(new HeadColliderBody(snake));
                        }
                        else
                        {
                            Debug.Log(bodySegment + "被吃掉了");
                            //蛇头比对方等级高，从被撞处截断
                            gameState.CommandModule.AddCommand(new IntoFoodCommand(otherSnake, bodySegment.name));
                        }
                    }
                }
            }
        }

        // 处理销毁队列,只需要从容器中去除即可
        while (removeQueue.Count > 0)
        {
            Snake snake = removeQueue.Dequeue();
            snakeList.Remove(snake);
        }
    }

    /// <summary>
    /// 蛇在这个脚本中被销毁的逻辑，已经封装好
    /// </summary>
    void DestroySnake(Snake snake)
    {
        //加入销毁缓存队列
        removeQueue.Enqueue(snake);
        //在世界管理类中删除
        World.Instance.AddToDestoryObjectBuffer(snake.Id);
    }

    public void Destroy()
    {
        // 销毁所有蛇
    }

}
