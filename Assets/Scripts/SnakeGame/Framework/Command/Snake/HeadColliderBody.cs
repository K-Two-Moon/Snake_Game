using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class HeadColliderBody : ICommand
{
    private Snake snake;

    public HeadColliderBody(Snake snake)
    {
        this.snake = snake;
    }

    public void Execute()
    {
        //List<Transform> list = snake.list;
        // // 取出尾部
        // Transform tail = snake.list[list.Count - 1];
        // //尾部退后一步
        // tail.position = tail.position - tail.forward * 1;
        // for (int i = 0; i < list.Count - 1; i++)
        // {
        //     list[i].position = list[i + 1].position;
        // }


        snake.head.position = snake.head.position - snake.head.forward * 2;
        //旋转180度
        snake.data.direcction = Quaternion.Euler(0, 180, 0) * snake.data.direcction;
    }
}
