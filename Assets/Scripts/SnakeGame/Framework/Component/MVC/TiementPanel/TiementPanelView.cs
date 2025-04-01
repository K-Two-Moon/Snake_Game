using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiementPanelView 
{
    public TiementPanel tiementPanel;

    public Text t_money;

    public Text t_addMoney;

    public TiementPanelView(IGameObject obj)
    {
        tiementPanel = obj as TiementPanel;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        t_money = GameObject.Find("SettlementPanel(Clone)/gold/t_gold").GetComponent<Text>();

        t_addMoney = GameObject.Find("SettlementPanel(Clone)/bg_rew/t_addGold").GetComponent<Text>();
    }
}
