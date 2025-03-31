using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGameObject
{
    protected GameObject obj;

    private uint id;

    protected Dictionary<ComponentType, IComponent> componentDict;

    public GameObject Obj => obj;
    public uint Id => id;

    public abstract void InitializeData(IData data);

    /// <summary>
    /// 重写后先调用后调用不影响
    /// </summary>
    public virtual void Create()
    {
        componentDict = new Dictionary<ComponentType, IComponent>();

        World.Instance.AddObject(this);
        OnCreate();
    }

    /// <summary>
    /// 基类没有逻辑
    /// </summary>
    protected virtual void OnCreate()
    {

    }

    /// <summary>
    /// 不能再组件中调用,否则很可能报错
    /// </summary>
    public virtual void Destroy()
    {
        //先销毁组件
        List<IComponent> list = new List<IComponent>(componentDict.Values);
        foreach (IComponent component in list)
        {
            component.Destroy();
        }
        //在World中移除对象
        // World.Instance.DestroyObjectToBuffer(id);

        componentDict.Clear();
        GameObject.Destroy(obj);
    }

    public void SetId(uint id)
    {
        this.id = id;
    }

    /// <summary>
    /// 子类实现自己的Update,父类逻辑为空
    /// </summary>
    /// <param name="deltaTime"></param>
    public virtual void Update(float deltaTime)
    {
        foreach (IComponent component in componentDict.Values)
        {
            component.Update();
        }
    }

    public void AddComponent(ComponentType type)
    {
        if (componentDict.ContainsKey(type))
        {
            Debug.LogError("Component already exists");
        }
        else
        {
            IComponent component = ComponentFactory.CreateProduct(type, this);
            componentDict.Add(type, component);
            component.Initialize();
        }
    }

    public void RemoveComponent(ComponentType type)
    {
        if (componentDict.ContainsKey(type))
        {
            IComponent component = componentDict[type];
            componentDict.Remove(type);
            component.Destroy();
        }
    }

    /// <summary>
    /// 获取自定义组件
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public IComponent GetComponent(ComponentType type)
    {
        if (componentDict.ContainsKey(type))
        {
            return componentDict[type];
        }
        else
        {
            //Debug.LogError("报空了");
            return null;
        }
    }
}

