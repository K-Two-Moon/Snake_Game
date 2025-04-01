using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Threading;

public class FoodEatComponent : IComponent
{
    Food food;
    List<Snake> SnakeList;


    public FoodEatComponent(ComponentType type, IGameObject obj) : base(type, obj)
    {
        food = obj as Food;
    }

    public override void Initialize()
    {
        base.Initialize();
        SnakeList = World.Instance.SnakeList;
    }

    public override void Destroy()
    {
        SnakeList = null;
        base.Destroy();
    }

    public override void Update()
    {
        base.Update();
        foreach (var snake in SnakeList)
        {
            Transform head = snake.head;
            Vector3 foodPosition = food.Obj.transform.position;

            try
            {
                float distance = Vector3.Distance(head.position, foodPosition);
                if (distance < 1.5f) // 吃到食物的距离阈值
                {
                    MoveFoodToHeadAsync(head, food);  // 传递 CancellationToken
                }
            }
            catch
            {
                Debug.Log(1);
            }
        }
    }

    // 移动食物到蛇头的异步方法
    async private void MoveFoodToHeadAsync(Transform snakeHead, Food food)
    {
        Debug.Log("吃到食物了");
        Vector3 targetPosition = snakeHead.position;
        float time = 0;

        // 获取 CancellationToken
        CancellationToken token = food.Obj.GetCancellationTokenOnDestroy();

        while (time < 1)
        {
            await UniTask.Yield(token);  // 使用 CancellationToken
            time += Time.deltaTime;
            // if (time >= 1)
            // {
            //     return;
            // }
            food.Obj.transform.position = Vector3.MoveTowards(food.Obj.transform.position, targetPosition, time);
        }
    }
}