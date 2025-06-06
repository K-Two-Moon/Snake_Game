public interface ISceneState
{
    string SceneName { get; }

    void Enter();
    void Exit();
    void Update();
}



public abstract class SceneState : ISceneState
{
    // 保护的场景状态控制器实例
    public SceneStateController controller;

    // 保护的场景名称字符串
    protected string sceneName;
    // 公开的只读属性，用于获取场景名称
    public string SceneName => sceneName;

    // 构造函数，初始化场景状态控制器
    public SceneState(SceneStateController controller)
    {
        this.controller = controller;
    }

    // 虚方法，用于进入场景状态时的处理
    public virtual void Enter()
    {
        World.Instance.Initialize();
    }

    // 虚方法，用于退出场景状态时的处理
    public virtual void Exit()
    {
        World.Instance.RemoveAllObject();
    }

    // 虚方法，用于场景状态更新时的处理
    public virtual void Update()
    {
        World.Instance.Update();
    }
}
