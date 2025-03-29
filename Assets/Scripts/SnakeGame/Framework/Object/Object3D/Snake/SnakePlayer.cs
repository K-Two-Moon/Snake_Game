using System;
using System.Diagnostics;

public class SnakePlayer : Snake
{
    public override void InitializeData(IData dataConfig)
    {
        base.InitializeData(dataConfig);
        
        data.PlayerInitLevel(PlayerSneakDataSingleton.Instance.playerData.initStartlevel,PlayerSneakDataSingleton.Instance.playerData.speed,PlayerSneakDataSingleton.Instance.playerData.Upgrade);
    }



    public override void Destroy()
    {
        base.Destroy();
    }

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

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }


}
