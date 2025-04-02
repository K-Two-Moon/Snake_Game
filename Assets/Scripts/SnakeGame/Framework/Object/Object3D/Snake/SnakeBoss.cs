using UnityEngine;

public class SnakeBoss : Snake
{
    // 定义边界值，用于确定蛇移动的范围边界
    private const float Boundary = 50f;
    // 定义身体部分之间的间距，用于控制蛇身体各部分之间的距离
    private const float BodySpacing = 3f;
    // 当前角落的索引，用于跟踪蛇当前位于哪个角落
    private int currentCorner = 0;
    // 定义路径点数组，包含四个角落的坐标，用于蛇的移动路径
    private Vector3[] pathPoints = new Vector3[4]
    {
        new Vector3(Boundary, 0, Boundary),
        new Vector3(-Boundary, 0, Boundary),
        new Vector3(-Boundary, 0, -Boundary),
        new Vector3(Boundary, 0, -Boundary)
    };

    public override void Create()
    {
        // 设置Boss蛇的超长身体
        data.bodyLength = (uint)(4 * Boundary * 2 / BodySpacing);
        base.Create();

        // 计算矩形周长
        float perimeter = 4 * 2 * Boundary;
        // 计算每个身体部分在矩形上的位置
        for (int i = 1; i < list.Count; i++)
        {
            float distanceAlongPath = (i-1) * BodySpacing;
            float normalizedDistance = distanceAlongPath / perimeter;
            
            // 计算在矩形上的位置
            Vector3 position;
            if (normalizedDistance < 0.25f) // 第一条边
            {
                float t = normalizedDistance / 0.25f;
                position = Vector3.Lerp(
                    new Vector3(Boundary, 0, Boundary),
                    new Vector3(-Boundary, 0, Boundary),
                    t);
            }
            else if (normalizedDistance < 0.5f) // 第二条边
            {
                float t = (normalizedDistance - 0.25f) / 0.25f;
                position = Vector3.Lerp(
                    new Vector3(-Boundary, 0, Boundary),
                    new Vector3(-Boundary, 0, -Boundary),
                    t);
            }
            else if (normalizedDistance < 0.75f) // 第三条边
            {
                float t = (normalizedDistance - 0.5f) / 0.25f;
                position = Vector3.Lerp(
                    new Vector3(-Boundary, 0, -Boundary),
                    new Vector3(Boundary, 0, -Boundary),
                    t);
            }
            else // 第四条边
            {
                float t = (normalizedDistance - 0.75f) / 0.25f;
                position = Vector3.Lerp(
                    new Vector3(Boundary, 0, -Boundary),
                    new Vector3(Boundary, 0, Boundary),
                    t);
            }

            list[i].position = position;
            
            // 设置朝向(指向下一个点)
            Vector3 nextPos = i < list.Count - 1 ? list[i+1].position : list[0].position;
            Vector3 direction = (nextPos - list[i].position).normalized;
            if (direction != Vector3.zero)
            {
                list[i].rotation = Quaternion.LookRotation(direction);
            }
        }
    }


    public override void Update(float deltaTime)
    {
        if (!obj) return;

        // 更新Boss蛇位置
        HeadMove(deltaTime);
        BodyAndTailMove(deltaTime);

        base.Update(deltaTime);
    }

    private void HeadMove(float deltaTime)
    {
        Transform head = list[0];

        // 计算当前目标点方向
        Vector3 targetDirection = (pathPoints[currentCorner] - head.position).normalized;

        // 更新蛇头方向
        head.rotation = Quaternion.LookRotation(targetDirection);

        // 蛇头移动
        head.position += head.forward * data.moveSpeed * deltaTime;

        // 检查是否到达当前路径点
        if (Vector3.Distance(head.position, pathPoints[currentCorner]) < 1f)
        {
            currentCorner = (currentCorner + 1) % 4;
        }
    }

    private void BodyAndTailMove(float deltaTime)
    {
        // 计算头部在矩形路径上的进度
        float headProgress = (float)currentCorner / 4 + 
                           Vector3.Distance(list[0].position, pathPoints[currentCorner]) / 
                           (4 * 2 * Boundary);

        // 更新每个身体部分的位置
        for (int i = 1; i < list.Count; i++)
        {
            // 计算该身体部分在矩形上的位置(相对于头部的偏移)
            float bodyProgress = headProgress - (i * BodySpacing) / (4 * 2 * Boundary);
            bodyProgress = Mathf.Repeat(bodyProgress, 1.0f);

            // 计算在矩形上的位置
            Vector3 position;
            if (bodyProgress < 0.25f) // 第一条边
            {
                float t = bodyProgress / 0.25f;
                position = Vector3.Lerp(pathPoints[0], pathPoints[1], t);
            }
            else if (bodyProgress < 0.5f) // 第二条边
            {
                float t = (bodyProgress - 0.25f) / 0.25f;
                position = Vector3.Lerp(pathPoints[1], pathPoints[2], t);
            }
            else if (bodyProgress < 0.75f) // 第三条边
            {
                float t = (bodyProgress - 0.5f) / 0.25f;
                position = Vector3.Lerp(pathPoints[2], pathPoints[3], t);
            }
            else // 第四条边
            {
                float t = (bodyProgress - 0.75f) / 0.25f;
                position = Vector3.Lerp(pathPoints[3], pathPoints[0], t);
            }

            list[i].position = position;

            // 设置朝向(指向下一个点)
            Vector3 nextPos;
            if (bodyProgress < 0.25f) nextPos = pathPoints[1];
            else if (bodyProgress < 0.5f) nextPos = pathPoints[2];
            else if (bodyProgress < 0.75f) nextPos = pathPoints[3];
            else nextPos = pathPoints[0];

            Vector3 direction = (nextPos - position).normalized;
            if (direction != Vector3.zero)
            {
                list[i].rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}