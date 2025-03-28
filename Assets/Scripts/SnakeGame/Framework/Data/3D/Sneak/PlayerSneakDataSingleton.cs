using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSneakDataSingleton : Singleton<PlayerSneakDataSingleton>
{
    public PlayerSingletonData playerData = new PlayerSingletonData();

    public int initStartlevel;//初始等级

    public int speed;//初始速度

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

}

public class PlayerSingletonData
{
    public int initStartlevel;//初始等级

    public int speed;//初始速度

    public int Upgrade;//初始进攻范围
}