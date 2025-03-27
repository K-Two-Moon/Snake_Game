public abstract class Snake : Object3D
{
    SnakeData data;
    public override void InitializeData(IData data)
    {
        this.data = data as SnakeData;
    }
}

public class SnakePlayer : Snake
{
    protected override void OnCreate()
    {
        //添加PlayerInput组件
        AddComponent(ComponentType.PlayerInput);
        base.OnCreate();
    }
}

public class SnakeEnemy : Snake
{
    protected override void OnCreate()
    {
        //添加EnemyAI组件
        AddComponent(ComponentType.EnemyAI);
        base.OnCreate();
    }
}
