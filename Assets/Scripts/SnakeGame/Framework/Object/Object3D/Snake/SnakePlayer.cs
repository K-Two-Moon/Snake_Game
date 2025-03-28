public class SnakePlayer : Snake
{
    protected override void OnCreate()
    {
        //添加PlayerInput组件
        AddComponent(ComponentType.PlayerInput);
        AddComponent(ComponentType.VirtualCameraComponent);
        base.OnCreate();
    }
}
