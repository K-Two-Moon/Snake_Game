using System;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public abstract class Snake : Object3D
{
    protected SnakeData data;
    List<Transform> list = new List<Transform>();

    public Snake ()
    {
        //创建父节点
        if (parent3D != null)
        {
            obj = new GameObject("蛇");
            obj.transform.SetParent(parent3D);
            obj.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogError("parent3D is null");
        }
        SnakeData snakeData = new SnakeData(ConfigManager.Instance.GetSnakeConfig(1) as SnakeConfig);
        InitializeData(snakeData);
        Create();
    }

    public override void InitializeData(IData data)
    {
        this.data = data as SnakeData;
    }

    public override void Create()
    {
        //创建头
        Transform head = GameObject.Instantiate(data.config.head).transform;
        head.SetParent(obj.transform);
        head.position = Vector3.zero;
        list.Add(head);

        //创建身体
        Transform body = GameObject.Instantiate(data.config.body).transform;
        body.SetParent(obj.transform);
        list.Add(body);

        //创建尾巴
        Transform tail = GameObject.Instantiate(data.config.tail).transform;
        tail.SetParent(obj.transform);
        list.Add(tail);


        //蛇各个身体部位的初始位置,大小
        for (int i = 1; i < list.Count; i++)
        {
            Transform child = list[i];
            child.localScale = Vector3.one;
            child.position = Vector3.back * data.followDistance;
        }


        base.Create();
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (!obj) return;

        //更新贪吃蛇位置

        HeadMove(deltaTime);
        BodyAndTailMove(deltaTime);

    }

    private void HeadMove(float deltaTime)
    {
        Transform head = list[0];

        //蛇头方向
        head.rotation = data.direcction;

        //蛇头移动
        head.position += head.forward * data.moveSpeed * deltaTime;
    }

    private void BodyAndTailMove(float deltaTime)
    {
        for (int i = 1; i < list.Count; i++)
        {
            Transform previousPart = list[i - 1];
            Transform currentPart = list[i];

            // 判断当前身体部分与前一部分之间的距离
            float distance = Vector3.Distance(currentPart.position, previousPart.position);
            if (distance > data.followDistance)
            {
                // 计算移动方向
                Vector3 direction = (previousPart.position - currentPart.position).normalized;
                // 平滑移动
                currentPart.position += direction * data.moveSpeed * deltaTime;

                // 计算目标旋转，使当前部分面向前一部分
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                // 平滑旋转到目标角度
                currentPart.rotation = Quaternion.RotateTowards(currentPart.rotation, targetRotation, data.rotationSpeed * deltaTime);

            }
        }
    }
}
