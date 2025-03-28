using UnityEngine;
using System; // 添加Cinemachine的命名空间引用

[Serializable]
public class PlayerInput : IComponent
{
    SnakePlayer player;

    // 摇杆预制体和所属画布
    public GameObject joystickPrefab;
    public Transform canvas;

    // 内部状态变量
    private GameObject currentJoystick;         // 当前实例化的摇杆
    private RectTransform joystickTransform;    // 摇杆RectTransform组件
    private Vector2 joystickStartPos;             // 摇杆初始位置（鼠标按下时的屏幕坐标）
    private float joystickRadius = 100f;          // 摇杆最大偏移半径，根据需求调整

    public PlayerInput(ComponentType type, IGameObject obj) : base(type, obj)
    {
        player = obj as SnakePlayer;

        RockerConfig config = Resources.Load<RockerConfig>("UIConfig/RockerConfig");
        joystickPrefab = config.rockerPrefab;

        canvas = GameObject.Find("Canvas").transform;
    }

    public override void Initialize()
    {
        base.Initialize();
    }
    public override void Destroy()
    {
        base.Destroy();
    }

    public override void Update()
    {
        InputMouseDowm();
        InputMouseUp();
        InputMouseUpdate();
    }

    /// <summary>
    /// 鼠标按下时创建摇杆
    /// </summary>
    private void InputMouseDowm()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        // 实例化摇杆预制体并将其挂载到画布下
        currentJoystick = GameObject.Instantiate(joystickPrefab, canvas);
        joystickTransform = currentJoystick.GetComponent<RectTransform>();

        // 将摇杆放置在当前鼠标点击位置
        joystickStartPos = Input.mousePosition;
        joystickTransform.position = joystickStartPos;

        // 可添加其他初始化逻辑（例如复位状态、通知控制模块等）
    }

    /// <summary>
    /// 持续更新摇杆位置并计算拖拽偏移
    /// </summary>
    private void InputMouseUpdate()
    {
        // 保证鼠标处于按下状态且摇杆已创建
        if (!Input.GetMouseButton(0) || currentJoystick == null) return;

        // 获取当前鼠标位置并计算与起始点的偏移量
        Vector2 currentPos = Input.mousePosition;
        Vector2 offset = currentPos - joystickStartPos;

        // 限制偏移量在设定的半径内
        Vector2 clampedOffset = Vector2.ClampMagnitude(offset, joystickRadius);

        // 更新摇杆位置，实现摇杆拖拽视觉效果
        joystickTransform.position = joystickStartPos + clampedOffset;

        // 基于偏移量计算移动方向（归一化后的向量）
        Vector2 moveDirection = clampedOffset.normalized;

        // 通知角色控制器更新角色移动方向
        // Vector2.zero不能被转换为四元数，是非法的
        if (moveDirection != Vector2.zero)
            player.data.SetDirection(moveDirection);

        // 调用角色控制逻辑，例如：
        // PlayerController.Instance.Move(moveDirection);
        // 这里可以加入企业级移动处理逻辑，确保数据与行为解耦，满足后续扩展需求
    }

    /// <summary>
    /// 鼠标松开时销毁摇杆
    /// </summary>
    private void InputMouseUp()
    {
        if (!Input.GetMouseButtonUp(0) || currentJoystick == null) return;

        // 可在销毁前添加状态重置、事件通知等逻辑
        GameObject.Destroy(currentJoystick);
        currentJoystick = null;
    }

}
