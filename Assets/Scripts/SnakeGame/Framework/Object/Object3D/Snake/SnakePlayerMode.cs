
/// <summary>
/// 玩家模型，没有移动和控制组件，只是一个预览效果
/// </summary>
public class SnakePlayerMode : Snake
{
    protected override void OnCreate()
    {
        //添加VirtualCameraComponent组件
        AddComponent(ComponentType.VirtualCameraComponent);
        base.OnCreate();
        RemoveComponent(ComponentType.SnakeLvUIView);
    }

    
}
