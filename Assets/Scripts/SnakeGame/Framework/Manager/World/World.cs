using System.Collections.Generic;
using UnityEngine;


public class World : Singleton<World>
{
    /// <summary>
    /// All objects in the world.
    /// </summary>
    Dictionary<uint, IGameObject> allObjectDict;
    /// <summary>
    /// 销毁队列缓存
    /// </summary>
    Queue<uint> destroyQueue;

    List<Snake> snakeList = new List<Snake>();

    public Snake maxSnake;//等级最高的蛇
    static uint nextId = 0;

    public void Initialize()
    {
        allObjectDict = new Dictionary<uint, IGameObject>();
        destroyQueue = new Queue<uint>();
    }


    public void AddObject(IGameObject obj)
    {
        allObjectDict.Add(nextId, obj);
        obj.SetId(nextId);
        nextId++;


        if (obj is Snake)
        {
            snakeList.Add(obj as Snake);
        }
    }

    void RemoveObject(uint id)
    {
        if (allObjectDict.ContainsKey(id))
        {
            IGameObject obj = allObjectDict[id];
            obj.Destroy();
            //世界中移除对象的注册
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


    public void RemoveAllObject()
    {
        List<IGameObject> list = new List<IGameObject>(allObjectDict.Values);
        foreach (IGameObject obj in list)
        {
            obj.Destroy();
        }

        allObjectDict.Clear();
        allObjectDict = null;
    }

    /// <summary>
    /// 外部销毁对象先放在这个缓存中，再帧最后统一销毁
    /// </summary>
    public void AddToDestoryObjectBuffer(uint id)
    {
        destroyQueue.Enqueue(id);
    }

    public GameObject king;
    public void Update()
    {
        float dayTime = Time.deltaTime;
        foreach (IGameObject item in allObjectDict.Values)
        {
            item.Update(dayTime);
        }

        //销毁缓存中的对象
        if (destroyQueue.Count > 0)
        {
            while (destroyQueue.Count > 0)
            {
                uint id = destroyQueue.Dequeue();
                RemoveObject(id);
            }
        }


        maxSnake = SortList();
        SnakeLvUIView snakeLvUIView = maxSnake.GetComponent(ComponentType.SnakeLvUIView) as SnakeLvUIView;
        if (snakeLvUIView != null)
        {
            if (king == null)
            {
                king = snakeLvUIView.AddKing();
            }
            king.transform.position = maxSnake.head.transform.position + Vector3.up * 2;
        }

    }

    #region 蛇的集合，用来排序，皇冠位置
    public void AddSnakeList(Snake snake)
    {
        snakeList.Add(snake);
    }

    public void RemoveSnakeList(Snake snake)
    {
        snakeList.Remove(snake);
    }

    public Snake SortList()
    {
        snakeList.Sort((a, b) => (int)(b.data.lv - a.data.lv));
        return snakeList[0];
    }

    #endregion
}
