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
    /// 蛇身体的缩放
    /// </summary>
    public float snakeScale;
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





    public SnakeData(SnakeConfig config, uint playerBodyLength = 1) //蛇只有7种颜色配置
    {
        //敌人蛇初始长度在玩家上下浮动
        this.config = config;
        // 根据随机偏移量调整玩家身体长度
        int bodyLengthOffset = Random.Range(-10, 10);
        int count = (int)playerBodyLength - bodyLengthOffset;
        // 确保身体长度至少为1
        if (count < 1)
        {
            count = 1;
        }
        bodyLength = (uint)count;

        // 设置跟随距离
        followDistance = 2f;

        // 设置移动速度
        moveSpeed = 5;

        // 设置旋转速度
        rotationSpeed = 360f;

        // 设置初始缩放
        snakeScale = 1f;

        // 初始化方向为单位四元数
        direcction = Quaternion.identity;

    }

    public void SetDirection(Vector2 v2)
    {
        //这里的v2是屏幕坐标系下的方向，需要转换成世界坐标系下的方向
        Vector3 v3 = new Vector3(v2.x, 0, v2.y);
        direcction = Quaternion.LookRotation(v3);
    }

    internal void PlayerInitLevel(int level, int speed, int upgrade)
    {

        bodyLength = (uint)level;
        moveSpeed = speed;

    }
}

