using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSneakDataSingleton : Singleton<PlayerSneakDataSingleton>
{
    public PlayerSingletonData playerData = new PlayerSingletonData();

    public int initStartlevel=1;//初始等级

    public int speed = 1;//初始速度

    public int Upgrade;//初始进攻范围

    public PlayerSingletonData GetData()
    {
        return playerData;
    }

    public PlayerSingletonData SetData(int level, int speed, int upgrade)
    {
        playerData.initStartlevel = level;
        playerData.speed = speed;
        playerData.Upgrade = upgrade;
        ConfigManager.Instance.SetPlayerSneakData();//每改变一次持久化一次
        return playerData;
    }

    public void SetMoneyData(int startMoney)
    {
        playerData.mianMoney = startMoney;
        ConfigManager.Instance.SetPlayerSneakData();//每改变一次持久化一次
    }

    /// <summary>
    /// 添加金币接口
    /// </summary>
    /// <param name="money"></param>
    public void SetAddMoney(int money)
    {
        playerData.mianMoney += money;
        MessageManager.Broadcast(CMD.ShowMonwy, money);//添加金币
    }
}

public class PlayerSingletonData
{
    public int initStartlevel;//初始等级

    public int speed;//初始速度

    public int Upgrade;//初始进攻范围

    public int mianMoney;//初始金币
}