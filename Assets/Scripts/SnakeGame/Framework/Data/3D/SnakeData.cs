

using UnityEngine;

public class SnakeData : IData
{
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
        //初始化蛇的身体长度为1
        bodyLength = 1;

        followDistance = 1.5f;

        moveSpeed = 1;

        rotationSpeed = 360f;

        direcction = Quaternion.identity;
    }

    public void SetDirection(Quaternion direction)
    {
        this.direcction = direction;
    }
}

