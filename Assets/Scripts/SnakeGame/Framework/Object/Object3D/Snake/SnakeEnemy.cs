using UnityEngine;

public class SnakeEnemy : Snake
{
    protected override void OnCreate()
    {
        //添加EnemyAI组件
        AddComponent(ComponentType.EnemyAI);
        //添加SnakeMove组件
        AddComponent(ComponentType.SnakeMove);
        base.OnCreate();
    }
}
