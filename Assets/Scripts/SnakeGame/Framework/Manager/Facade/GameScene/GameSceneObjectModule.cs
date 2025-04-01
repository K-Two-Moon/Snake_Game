using System.Collections.Generic;
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
    private List<Snake> snakeList = new List<Snake>();
    // Remove Buffer
    private Queue<Snake> removeQueue = new Queue<Snake>();

    // 碰撞判定的距离阈值，根据实际情况调整
    public float collisionThreshold = 0.5f;
    public void Initialize()
    {
        // 这里是我的测试代码======================

        Snake player = Object3DFactory.CreateProduct(Object3DType.SnakePlayer) as Snake;
        SnakeData snakeData = new SnakeData(ConfigManager.Instance.GetSnakeConfig(1) as SnakeConfig);
        player.InitializeData(snakeData);
        player.Create();
        snakeList.Add(player);

        for (int i = 2; i <= 7; i++)
        {
            Snake enemy = Object3DFactory.CreateProduct(Object3DType.SnakeEnemy) as Snake;
            SnakeData snakeDataEnemy = new SnakeData(ConfigManager.Instance.GetSnakeConfig((uint)i) as SnakeConfig);
            enemy.InitializeData(snakeDataEnemy);
            enemy.Create();
            snakeList.Add(enemy);
            //随机位置
            Vector2 v2 = Random.insideUnitCircle * 10;
            enemy.Obj.transform.position = new Vector3(v2.x, 0, v2.y);
        }
    }

    public void Update(float deltaTime)
    {
        CheckCollisions();
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

            // 对比其他蛇的头部和身体
            foreach (Snake otherSnake in snakeList)
            {
                // 排除自身碰撞检测
                if (otherSnake == snake)
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
        //在世界管理类中删除
        World.Instance.AddToDestoryObjectBuffer(snake.Id);
        //加入销毁缓存队列
        removeQueue.Enqueue(snake);
    }
}
