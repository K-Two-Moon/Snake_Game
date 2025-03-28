using UnityEngine;

public class EnemyAIComponent : IComponent
{
    SnakeEnemy enemy;

    public Vector2 currentDirection;   // 当前的随机方向
    private float timeToChangeDirection;  // 改变方向的随机时间间隔
    private float timeSinceLastChange;   // 距离上次改变方向的时间
    public EnemyAIComponent(ComponentType type, IGameObject obj) : base(type, obj)
    {
        enemy = obj as SnakeEnemy;
    }

    public override void Initialize()
    {
        //初始化随机时间间隔
        timeToChangeDirection = Random.Range(1f, 3f);
        //重置随机时间间隔
        timeSinceLastChange = 0f;
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    public override void Update()
    {
        // 累加自上次改变方向以来的时间
        timeSinceLastChange += Time.deltaTime;

        // 如果达到随机的改变方向时间间隔
        if (timeSinceLastChange >= timeToChangeDirection)
        {
            //重置随机时间间隔
            timeToChangeDirection = Random.Range(1f, 3f);

            // 重置计时器，准备下一次随机时间
            timeSinceLastChange = 0f;

            // 更新当前方向为一个随机方向
            currentDirection = GetRandomDirection2D();



            // 重新设置下次改变方向的时间间隔
            timeToChangeDirection = Random.Range(1f, 3f);

            // 通知敌人改变方向
            if (currentDirection != Vector2.zero)
                enemy.data.SetDirection(currentDirection);
        }
    }

    // 生成一个随机的二维方向向量
    private Vector2 GetRandomDirection2D()
    {
        // 随机生成一个0到360之间的角度
        float angle = Random.Range(0f, 360f);

        // 将角度转为弧度
        float rad = angle * Mathf.Deg2Rad;

        // 返回基于角度的单位向量（归一化）
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }
}


