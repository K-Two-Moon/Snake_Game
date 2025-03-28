using System;

public class SnakePlayer : Snake
{
    public override void InitializeData(IData dataConfig)
    {
        base.InitializeData(dataConfig);
        data.PlayerInitLevel(PlayerSneakDataSingleton.Instance.initStartlevel,PlayerSneakDataSingleton.Instance.speed,PlayerSneakDataSingleton.Instance.Upgrade);
    }



    public override void Destroy()
    {
        base.Destroy();

    }

    protected override void OnCreate()
    {
        //添加PlayerInput组件
        AddComponent(ComponentType.PlayerInput);
        AddComponent(ComponentType.VirtualCameraComponent);
        base.OnCreate();
    }


}
