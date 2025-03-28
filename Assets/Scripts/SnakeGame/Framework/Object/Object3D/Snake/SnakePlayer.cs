public class SnakePlayer : Snake
{
    protected override void OnCreate()
    {
        //添加PlayerInput组件
        AddComponent(ComponentType.PlayerInput);
        //添加虚拟相机组件
        AddComponent(ComponentType.VirtualCameraComponent);
        //添加SnakeMove组件
        AddComponent(ComponentType.SnakeMove);
        base.OnCreate();
    }
}
