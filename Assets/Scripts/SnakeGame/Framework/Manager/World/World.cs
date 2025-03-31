using System.Collections.Generic;
using UnityEngine;


public class World : Singleton<World>
{
    /// <summary>
    /// All objects in the world.
    /// </summary>
    Dictionary<uint, IGameObject> allObjectDict;

    List<Snake> snakeList = new List<Snake>();

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


        if(obj is Snake)
        {
            snakeList.Add(obj as Snake);
        }
    }

    public void RemoveObject(uint id)
    {
        if (allObjectDict.ContainsKey(id))
        {
            //移除对象
            allObjectDict.Remove(id);
        }
    }

    public void Destroy()
    {
        RemoveAllObject();
    }

    void RemoveAllObject()
    {
        foreach (var obj in allObjectDict.Values)
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
