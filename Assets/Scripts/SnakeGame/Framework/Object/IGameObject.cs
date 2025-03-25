using System.Collections.Generic;
using UnityEngine;

public abstract class IGameObject
{
    private GameObject obj;

    private uint id;

    protected Dictionary<ComponentType, IComponent> componentDict;

    public GameObject Obj => obj;
    public uint Id => id;

    public abstract void InitializeData(IData data);

    public virtual void Create()
    {
        componentDict = new Dictionary<ComponentType, IComponent>();

        World.Instance.AddObject(this);
        OnCreate();
    }

    protected virtual void OnCreate()
    {
        //子类中添加组件
        foreach (IComponent component in componentDict.Values)
        {
            component.Initialize();
        }
    }

    public virtual void Destroy()
    {
        //先销毁组件
        foreach (IComponent component in componentDict.Values)
        {
            component.Destroy();
        }
        //在World中移除对象
        World.Instance.RemoveObject(id);

        GameObject.Destroy(obj);
    }

    public void SetId(uint id)
    {
        this.id = id;
    }



    protected void AddComponent(ComponentType type)
    {
        if (componentDict.ContainsKey(type))
        {
            Debug.LogError("Component already exists");
        }
        else
        {
            IComponent component = ComponentFactory.CreateProduct(type, this);
            componentDict.Add(type, component);
        }
    }

    public void RemoveComponent(ComponentType type)
    {
        if (componentDict.ContainsKey(type))
        {
            componentDict.Remove(type);
        }
    }
}

