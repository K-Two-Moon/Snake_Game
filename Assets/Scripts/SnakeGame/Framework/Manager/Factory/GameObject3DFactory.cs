using UnityEngine;


/// <summary>
/// 产品类型
/// </summary>
public enum GameObject3DType
{
    /// <summary>
    /// 蛇(玩家)
    /// </summary>
    SnakePlayer,
    /// <summary>
    /// 蛇(玩家模型)
    /// </summary>
    SnakePlayerModle,
    /// <summary>
    /// 蛇(敌人)
    /// </summary>
    SnakeEnemy,
    /// <summary>
    /// 食物
    /// </summary>
    Food,

}


/// <summary>
/// 2D游戏对象工厂
/// </summary>
public static class GameObject3DFactory
{
    public static IGameObject CreateProduct(GameObject3DType type)
    {
        IGameObject product = null;
        product = SwitchBuilder(type);

        if (product == null)
            Debug.LogError("创建失败");

        // 返回产品
        return product;
    }

    private static IGameObject SwitchBuilder(GameObject3DType type) => type switch
    {
        GameObject3DType.SnakePlayer => new SnakePlayer(),
        GameObject3DType.SnakePlayerModle => new SnakePlayerMode(),
        GameObject3DType.SnakeEnemy => new SnakeEnemy(),

        _ => null,
    };
}


