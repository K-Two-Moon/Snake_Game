
using Cinemachine;

using UnityEngine;
/// <summary>
/// 虚拟相机组件
/// </summary>
public sealed class VirtualCameraComponent : IComponent
{
    SnakePlayer player;
    GameObject virtualCameraObj;
    /// <summary>
    /// 控制相机高度
    /// </summary>
    CinemachineTransposer cinemachineTransposer;
    public VirtualCameraComponent(ComponentType type, IGameObject obj) : base(type, obj)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        player = obj as SnakePlayer;
        CreateVlam();

    }
    /// <summary>
    /// 创建虚拟相机
    /// </summary>
    private void CreateVlam()
    {
        // 创建虚拟相机
        virtualCameraObj = new GameObject("VirtualCamera");
        CinemachineVirtualCamera vlam = virtualCameraObj.AddComponent<CinemachineVirtualCamera>();

        //跟随蛇的头部
        vlam.Follow = player.head;
        vlam.LookAt = player.head;

        // 添加并配置 Transposer（Body）组件
        cinemachineTransposer = vlam.AddCinemachineComponent<CinemachineTransposer>();
        cinemachineTransposer.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
        cinemachineTransposer.m_FollowOffset = new Vector3(0, 20, 0);

        //添加并配置 Composer（Aim）组件
        CinemachineComposer composer = vlam.AddCinemachineComponent<CinemachineComposer>();
        composer.m_TrackedObjectOffset = new Vector3(0, 2, 0);
        composer.m_LookaheadTime = 0.1f;
    }

    public override void Destroy()
    {
        // 销毁虚拟相机
        GameObject.Destroy(cinemachineTransposer);
        base.Destroy();
    }
}