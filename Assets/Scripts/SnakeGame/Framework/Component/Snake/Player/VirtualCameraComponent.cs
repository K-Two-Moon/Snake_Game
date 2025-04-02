
using Cinemachine;

using UnityEngine;
/// <summary>
/// 虚拟相机组件
/// </summary>
public sealed class VirtualCameraComponent : IComponent
{
    Snake player;
    GameObject virtualCameraObj;
    /// <summary>
    /// 控制相机高度
    /// </summary>
    CinemachineTransposer cinemachineTransposer;
    public VirtualCameraComponent(ComponentType type, IGameObject obj) : base(type, obj)
    {
        player = obj as Snake;
    }

    public override void Initialize()
    {
        base.Initialize();
        CreateVlam();
    }
    /// <summary>
    /// 创建虚拟相机
    /// </summary>
    private void CreateVlam()
    {
        // 创建虚拟相机
        virtualCameraObj = new GameObject("VirtualCamera");
        virtualCameraObj.transform.rotation = Quaternion.Euler(90, 0, 0);
        CinemachineVirtualCamera vlam = virtualCameraObj.AddComponent<CinemachineVirtualCamera>();

        //跟随蛇的头部
        vlam.Follow = player.head;

        //vlam.LookAt = player.head;   看向会导致跟着旋转


        // 添加并配置 Transposer（Body）组件
        // 正确获取 Transposer
        cinemachineTransposer = vlam.GetCinemachineComponent<CinemachineTransposer>();
        if (cinemachineTransposer == null)
        {
            cinemachineTransposer = vlam.AddCinemachineComponent<CinemachineTransposer>();
        }
        cinemachineTransposer.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
        cinemachineTransposer.m_FollowOffset = new Vector3(0, 50, 0);

        //添加并配置 Composer（Aim）组件
        CinemachineComposer composer = vlam.AddCinemachineComponent<CinemachineComposer>();
        composer.m_TrackedObjectOffset = new Vector3(0, 2, 0);
        composer.m_LookaheadTime = 0.1f;
    }

    public override void Destroy()
    {
        // 销毁虚拟相机
        GameObject.Destroy(virtualCameraObj);
        base.Destroy();
    }
}