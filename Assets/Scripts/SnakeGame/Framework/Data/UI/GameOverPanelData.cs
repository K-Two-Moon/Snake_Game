using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelData : IData
{
    //结算面板数据
    public GameOverConfing config;

    public int money;

    public int addMoney;
    //传初始化数据
    public GameOverPanelData(GameOverConfing config)
    {
        this.config  = config;
    }
}
