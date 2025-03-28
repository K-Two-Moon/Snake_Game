using UnityEngine;

public enum ComponentType
{
    //玩家组件
    PlayerInput,
    VirtualCameraComponent,


    //敌人组件
    EnemyAI,

    //蛇的移动算法组件
    SnakeMove,



    //UI的C层组件
    MainPanelController
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
        //=================玩家组件
        ComponentType.PlayerInput => new PlayerInputComponent(type, obj),
        ComponentType.VirtualCameraComponent => new VirtualCameraComponent(type, obj),


        //==================敌人组件
        ComponentType.EnemyAI => new EnemyAIComponent(type, obj),

        //================蛇的移动算法组件
        ComponentType.SnakeMove => new SnakeMoveComponent(type, obj),


        //================UI的C层作为组件
        ComponentType.MainPanelController => new MainPanelController(type, obj),
        _ => null
    };
}


