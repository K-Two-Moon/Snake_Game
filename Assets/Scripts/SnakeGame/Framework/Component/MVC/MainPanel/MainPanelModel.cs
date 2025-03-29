using System.Collections.Generic;
using Codice.CM.Common;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public class MainPanelModel
{
    private MainPanel obj;

    public MainPanelData data;

    public List<ItemData> items = new List<ItemData>();//三个Item的数据

    public int speed = 1;//初始速度
    public int level = 1;//初始等级
    public int Upgrade = 1;//初始进攻范围

    PlayerSingletonData info;

    public MainPanelModel(IGameObject obj)
    {
        this.obj = obj as MainPanel;
        data = this.obj.data;
    }

    /// <summary>
    /// 这里获取数据
    /// </summary>
    internal void Initialize()
    {
        info = JsonConvert.DeserializeObject<PlayerSingletonData>(Resources.Load<TextAsset>("playerInfo").text);
        items.Add(new ItemData { id = 1, level = info.initStartlevel, sum = 0, item = data.confing.itemArray[0] });
        items.Add(new ItemData { id = 2, level = info.speed, sum = 0, item = data.confing.itemArray[1] });
        items.Add(new ItemData { id = 3, level = info.Upgrade, sum = 0, item = data.confing.itemArray[2] });
        speed = info.speed;
        level = info.initStartlevel;
        Upgrade = info.Upgrade;
        data.money = 50000;//测试初始金币
        SetSumNum();
        
        UpdataPlayerData();//第一次 更新Player单例的数据
        Debug.Log("11111");
        ConfigManager.Instance.SetPlayerSneakData();//持久化
    }

    /// <summary>
    /// 添加金币的方法
    /// </summary>
    /// <param name="num"></param>
    public void AddMoney(int num)
    {
        data.money += num;
        //发消息到c

    }

    /// <summary>
    /// 添加钻石的方法
    /// </summary>
    /// <param name="num"></param>
    public void AddDiamond(int num)
    {
        data.diamond += num;
        //发消息到c

    }

    /// <summary>
    /// 计算当前等级消耗金币
    /// </summary>
    public void SetSumNum()
    {
        foreach (var item in items)
        {
            item.sum = item.level * item.item.level_Num;
        }
    }

    /// <summary>
    /// 添加等级的方法
    /// </summary>
    public void AddLevel(int index)
    {
        if (items[index].sum <= data.money)
        {
            items[index].level++;
            data.money -= items[index].sum;
            SetSumNum();
            if (index == 0)
            {
                level += 1;
            }
            if (index == 1)
            {
                speed += 1;
            }
            if (index == 2)
            {
                Upgrade += 1;
            }
            UpdataPlayerData();
        }
        else
        {
            //金币不足
        }
        MessageManager.Broadcast(CMD.ShowLevel);
    }

    /// <summary>
    /// 更新玩家的单例数据
    /// </summary>
    public void UpdataPlayerData()
    {
        Debug.Log("这是初始化，应该为1"+speed);
        PlayerSneakDataSingleton.Instance.SetData(level,speed,Upgrade);
    }
}

public class ItemData
{
    public int id;
    public int level;
    public int sum;
    public Item item = new Item();
}