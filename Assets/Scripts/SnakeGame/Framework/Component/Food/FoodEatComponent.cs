using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Threading;
using System;

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
            if(head!=null)
            {
                float distance = Vector3.Distance(head.position, foodPosition);
                if (distance < 1.5f) // 吃到食物的距离阈值
                {
                    MoveFoodToHeadAsync(snake, food);  // 传递 CancellationToken
                }
            }
        }
    }

    // 移动食物到蛇头的异步方法
    async private void MoveFoodToHeadAsync(Snake snake, Food food)
    {
        if (snake == null || snake.head == null || food == null || food.Obj == null)
        {
            Debug.LogWarning("MoveFoodToHeadAsync: 参数不合法");
            return;
        }

        // Debug.Log("吃到食物了");
        float time = 0;
        var token = food.Obj.GetCancellationTokenOnDestroy();

        var startPosition = food.Obj.transform.position;
        var endPosition = snake.head.position;

        while (time < 1f && !token.IsCancellationRequested)
        {
            time += Time.deltaTime * 10f;
            food.Obj.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            await UniTask.Yield(token).SuppressCancellationThrow();
        }

        if (snake != null && !token.IsCancellationRequested)
        {
            snake.InsertBodyPart();
        }
        World.Instance.AddToDestoryObjectBuffer(food.Id);
        //发送蛇涌动命令
        MessageManager.Broadcast<ICommand>(CMD.AddToCommandQueue, new SnakeSurgeCommand(snake));
    }

}