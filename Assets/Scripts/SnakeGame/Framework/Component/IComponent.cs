/// <summary>
/// 组件
/// </summary>
public abstract class IComponent
{
    /// <summary>
    /// 通过中介模式与GameObject交互
    /// </summary>
    protected IGameObject obj;
    protected ComponentType type;

    protected IComponent(ComponentType type, IGameObject obj)
    {
        this.obj = obj;
        this.type = type;
    }

    /// <summary>
    /// 基类方法没有逻辑
    /// </summary>
    public virtual void Initialize()
    {

    }
    public virtual void Destroy()
    {
        obj.RemoveComponent(type);
    }
    /// <summary>
    /// 基类方法没有逻辑
    /// </summary>
    public virtual void Update()
    {

    }
}
