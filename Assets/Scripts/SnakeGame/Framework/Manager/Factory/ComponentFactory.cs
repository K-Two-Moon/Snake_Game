using UnityEngine;

public enum ComponentType
{
    /// <summary>
    /// 玩家组件
    /// </summary>
    PlayerInput,
    VirtualCameraComponent,


    /// <summary>
    /// 敌人组件
    /// </summary>
    EnemyAI,

    /// <summary>
    /// 蛇的移动算法组件
    /// </summary>
    SnakeMove,
    /// <summary>
    /// 蛇头检测前方对象组件
    /// </summary>
    DetectObjectAheadComponent,


    /// <summary>
    /// UI的C层组件
    /// </summary>
    MainPanelController,
    //UI的C层组件


    SnakeLvUIView


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

        //================蛇头检测前方对象组件
        ComponentType.DetectObjectAheadComponent => new DetectObjectAheadComponent(type, obj),


        //================UI的C层作为组件
        ComponentType.MainPanelController => new MainPanelController(type, obj),

        ComponentType.SnakeLvUIView => new SnakeLvUIView(type, obj),
        _ => null
    };
}


