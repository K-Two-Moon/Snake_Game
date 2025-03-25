
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

    public virtual void Initialize()
    {

    }
    public virtual void Destroy()
    {
        obj.RemoveComponent(type);
    }
    public virtual void Update()
    {

    }
}