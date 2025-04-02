using System.Collections.Generic;
using UnityEngine;

public abstract class Snake : Object3D
{
    public SnakeData data;
    public List<Transform> list = new List<Transform>();
    /// <summary>
    /// 蛇头
    /// </summary>
    public Transform head;

    public Snake()
    {
        //创建父节点
        if (parent != null)
        {
            obj = new GameObject("蛇");
            obj.transform.SetParent(parent);
            obj.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogError("parent3D is null");
        }
    }

    public override void InitializeData(IData data)
    {
        this.data = data as SnakeData;
        World.Instance.AddSnakeList(this);
    }

    /// <summary>
    /// 必须调用
    /// </summary>
    public override void Destroy()
    {
        base.Destroy();
        World.Instance.RemoveSnakeList(this);
    }

    public override void Create()
    {
        //创建头
        head = Object.Instantiate(data.config.head).transform;
        if (head == null)
        {
            Debug.Log("head is null");
        }
        head.SetParent(obj.transform);
        head.position = Vector3.zero;
        list.Add(head);

        //创建身体
        for (int i = 0; i < data.bodyLength; i++)
        {
            Transform body = Object.Instantiate(data.config.body).transform;
            body.name = "body" + i;
            body.SetParent(obj.transform);
            list.Add(body);
        }

        //创建尾巴
        Transform tail = Object.Instantiate(data.config.tail).transform;
        tail.SetParent(obj.transform);
        list.Add(tail);


        head.localScale = Vector3.one * data.snakeScale;
        //蛇各个身体部位的初始位置,大小
        for (int i = 1; i < list.Count; i++)
        {
            Transform child = list[i];
            child.localScale = Vector3.one * data.snakeScale;
            child.position = list[i - 1].position - (list[i - 1].forward * data.snakeScale);
        }

        base.Create();
    }

    protected override void OnCreate()
    {
        AddComponent(ComponentType.SnakeLvUIView);
        //AddComponent(ComponentType.DetectObjectAheadComponent);
        base.OnCreate();
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (!obj) return;

        //更新贪吃蛇位置

        //HeadMove(deltaTime);
        //BodyAndTailMove(deltaTime);

        //每帧都更新等级
        data.lv = data.bodyLength * 10 - 9;
    }

    private void HeadMove(float deltaTime)
    {
        Transform head = list[0];

        //蛇头方向
        head.rotation = data.direcction;

        //蛇头移动
        head.position += head.forward * data.moveSpeed * deltaTime;
    }


    /// <summary>
    /// 新算法
    /// </summary>
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
        Debug.Log(data.moveSpeed);
    }

    /// <summary>
    /// 在蛇头后面插入新的身体部位
    /// </summary>
    public void InsertBodyPart()
    {
        if (data == null || data.config == null || data.config.body == null || obj == null)
        {
            return;
        }


        // 创建新的身体部位
        Transform newBody = Object.Instantiate(data.config.body).transform;
        newBody.name = "body" + (data.bodyLength + 1);
        newBody.SetParent(obj.transform);

        // 将新身体插入到尾巴前面

        list.Insert(list.Count - 1, newBody);

        // 更新蛇的长度
        data.bodyLength++;

        // 设置新身体的位置(放在蛇尾巴的位置)

        newBody.position = list[list.Count - 1].position;
        newBody.rotation = list[list.Count - 1].rotation;
    }
}
