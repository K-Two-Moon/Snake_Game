using System.Collections.Generic;
using UnityEngine;


public class World : Singleton<World>
{
    /// <summary>
    /// All objects in the world.
    /// </summary>
    Dictionary<uint, IGameObject> allObjectDict;

    static uint nextId = 0;

    public void Initialize()
    {
        allObjectDict = new Dictionary<uint, IGameObject>();
    }

    public void AddObject(IGameObject obj)
    {
        allObjectDict.Add(nextId, obj);
        obj.SetId(nextId);
        nextId++;
    }

    public void RemoveObject(uint id)
    {
        if (allObjectDict.ContainsKey(id))
        {
            //移除对象
            allObjectDict.Remove(id);
        }
    }

    public IGameObject GetObjectById(uint id)
    {
        if (allObjectDict.ContainsKey(id))
        {
            return allObjectDict[id];
        }
        else
        {
            Debug.LogError("没有这个对象");
            return null;
        }
    }

    public void Destroy()
    {
        RemoveAllObject();
    }

    void RemoveAllObject()
    {
        List<IGameObject> list = new List<IGameObject>(allObjectDict.Values);
        foreach (IGameObject obj in list)
        {
            obj.Destroy();
        }

        allObjectDict.Clear();
        allObjectDict = null;
    }


    public void Update()
    {
        float dayTime = Time.deltaTime;
        foreach (IGameObject item in allObjectDict.Values)
        {
            item.Update(dayTime);
        }
    }
}
