using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelmodel
{
    private GameOverPanel obj;

    public GameOverPanelData data;

    public GameOverPanelmodel(IGameObject obj)
    {
        this.obj = obj as GameOverPanel;
        data = this.obj.data;
    }
    
    /// <summary>
    /// 这里获取数据
    /// </summary>
    internal void Initialize()
    {
        data.money = PlayerSneakDataSingleton.Instance.playerData.mianMoney;
        data.addMoney = PlayerSneakDataSingleton.Instance.playerData.initStartlevel * 133;
    }
}
