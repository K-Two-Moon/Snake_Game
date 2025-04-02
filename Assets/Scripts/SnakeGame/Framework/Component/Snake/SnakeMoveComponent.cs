using System.Collections.Generic;
using UnityEngine;

public class SnakeMoveComponent : IComponent
{
    Snake snake;
    SnakeData data;
    public List<Transform> list;
    public SnakeMoveComponent(ComponentType type, IGameObject obj) : base(type, obj)
    {
        snake = obj as Snake;
        data = snake.data;
        list = snake.list;
    }

    /// <summary>
    /// 蛇移动算法
    /// </summary>
    public override void Update()
    {
        float deltaTime = Time.deltaTime;
        HeadMove(deltaTime);
        BodyAndTailMove(deltaTime);
    }

    private void HeadMove(float deltaTime)
    {
        Transform head = snake.head;

        //蛇头方向
        head.rotation = data.direcction;

        //蛇头移动
        head.position += head.forward * data.moveSpeed * deltaTime;

        //边界检查
        if (Mathf.Abs(head.position.x) > 50 || Mathf.Abs(head.position.z) > 50)
        {
            //计算指向原点的方向(忽略y轴)
            Vector3 toOrigin = new Vector3(-head.position.x, 0, -head.position.z).normalized;
            data.direcction = Quaternion.LookRotation(toOrigin);
        }
    }

    private void BodyAndTailMove(float deltaTime)
    {
        for (int i = 1; i < list.Count; i++)
        {
            Transform previousPart = list[i - 1];
            Transform currentPart = list[i];

            // 使用插值使当前位置平滑地过渡到前一部分的位置
            Vector3 targetPosition = previousPart.position;
            // 平滑移动，使用Lerp来实现
            currentPart.position = Vector3.Lerp(currentPart.position, targetPosition, data.moveSpeed / 1.5f * deltaTime);

            // 计算目标旋转，使当前部分面向前一部分
            Vector3 direction = (previousPart.position - currentPart.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // 平滑旋转到目标角度
                currentPart.rotation = Quaternion.RotateTowards(currentPart.rotation, targetRotation, data.rotationSpeed * deltaTime * 2);
            }
        }
    }
}
