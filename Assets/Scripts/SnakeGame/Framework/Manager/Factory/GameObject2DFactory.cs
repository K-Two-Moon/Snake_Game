using UnityEngine;


/// <summary>
/// 产品类型
/// </summary>
public enum GameObject2DType
{
    /// <summary>
    /// 蛇(玩家)
    /// </summary>
    SnakePlayer,
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
public static class GameObject2DFactory
{
    public static IGameObject CreateProduct(GameObject2DType type)
    {
        IGameObject product = null;
        product = SwitchBuilder(type);

        if (product == null)
            Debug.LogError("创建失败");

        // 返回产品
        return product;
    }

    private static IGameObject SwitchBuilder(GameObject2DType type) => type switch
    {
        GameObject2DType.SnakePlayer => new SnakePlayer(),
        GameObject2DType.SnakeEnemy => new SnakeEnemy(),

        _ => null,
    };
}


