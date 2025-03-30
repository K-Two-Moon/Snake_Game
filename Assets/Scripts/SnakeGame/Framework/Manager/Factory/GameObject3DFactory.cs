using UnityEngine;


/// <summary>
/// 产品类型
/// </summary>
public enum Object3DType
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
/// 3D对象工厂
/// </summary>
public static class Object3DFactory
{
    public static IGameObject CreateProduct(Object3DType type)
    {
        IGameObject product = null;
        product = SwitchBuilder(type);

        if (product == null)
            Debug.LogError("创建失败");

        // 返回产品
        return product;
    }

    private static IGameObject SwitchBuilder(Object3DType type) => type switch
    {
        Object3DType.SnakePlayer => new SnakePlayer(),
        Object3DType.SnakePlayerModle => new SnakePlayerMode(),
        Object3DType.SnakeEnemy => new SnakeEnemy(),

        Object3DType.Food => new Food(),
        _ => null,
    };
}


