

using System;
using log4net.Core;
using UnityEngine;

public class SnakeData : IData
{
    /// <summary>
    /// 蛇的等级
    /// </summary>
    public uint lv;
    /// <summary>
    /// 蛇身体的长度
    /// </summary>
    public uint bodyLength;
    /// <summary>
    /// 蛇每节身体之间的距离
    /// </summary>
    public float followDistance;
    /// <summary>
    /// 蛇的移动速度
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// 身体旋转速度（角度/秒）
    /// </summary>
    public float rotationSpeed;
    /// <summary>
    /// 蛇头的方向
    /// </summary>
    public Quaternion direcction;
    /// <summary>
    /// 里面是蛇身体的预制体
    /// </summary>
    public SnakeConfig config;





    public SnakeData(SnakeConfig config) //蛇只有7种颜色配置
    {
        this.config = config;

        lv = 1;

        //初始化蛇的身体长度为2
        bodyLength = 20;

        followDistance = 2f;

        moveSpeed = 5;

        rotationSpeed = 360f;

        direcction = Quaternion.identity;

        lv = bodyLength * 10 - 9;

    }

    public void SetDirection(Vector2 v2)
    {
        //这里的v2是屏幕坐标系下的方向，需要转换成世界坐标系下的方向
        Vector3 v3 = new Vector3(v2.x, 0, v2.y);
        direcction = Quaternion.LookRotation(v3);
    }

    internal void PlayerInitLevel(int level,int speed,int upgrade)
    {
        
        bodyLength = (uint)level;
        moveSpeed=speed;
        lv = bodyLength * 10 - 9;
    }
}

