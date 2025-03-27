public class SnakeEnemy : Snake
{
    protected override void OnCreate()
    {
        //添加EnemyAI组件
        AddComponent(ComponentType.EnemyAI);
        base.OnCreate();
    }
}
