using UnityEngine;

public enum ComponentType
{
    PlayerInput,
    EnemyAI,
}

/// <summary>
/// 组件工厂
/// </summary>
public static class ComponentFactory
{
    public static IComponent CreateProduct(ComponentType type, IGameObject obj)
    {
        IComponent product = null;
        product = SwitchBuilder(type, obj);

        if (product == null)
            Debug.LogError("创建失败");

        // 返回产品
        return product;
    }

    private static IComponent SwitchBuilder(ComponentType type, IGameObject obj) => type switch
    {
        ComponentType.PlayerInput => new PlayerInput(type, obj),
        ComponentType.EnemyAI => new EnemyAI(type, obj),
        _ => null
    };
}


