using UnityEngine;
using Cinemachine; // 添加Cinemachine的命名空间引用

public class PlayerInput : IComponent
{
    SnakePlayer player;
    /// <summary>
    /// 控制相机高度
    /// </summary>
    CinemachineTransposer cinemachineTransposer;
    public PlayerInput(ComponentType type, IGameObject obj) : base(type, obj)
    {
        player = obj as SnakePlayer;
    }

    public override void Initialize()
    {
        base.Initialize();
        // 创建虚拟相机
        GameObject virtualCameraObj = new GameObject("VirtualCamera");
        CinemachineVirtualCamera vlam = virtualCameraObj.AddComponent<CinemachineVirtualCamera>();

        vlam.Follow = player.Obj.transform;
        vlam.LookAt = player.Obj.transform;


        // 添加并配置 Transposer（Body）组件
        cinemachineTransposer = vlam.AddCinemachineComponent<CinemachineTransposer>();
        cinemachineTransposer.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
        cinemachineTransposer.m_FollowOffset = new Vector3(0, 20, 0);

        //添加并配置 Composer（Aim）组件
        CinemachineComposer composer = vlam.AddCinemachineComponent<CinemachineComposer>();
        composer.m_TrackedObjectOffset = new Vector3(0, 2, 0);
        composer.m_LookaheadTime = 0.1f;
    }

 
}
