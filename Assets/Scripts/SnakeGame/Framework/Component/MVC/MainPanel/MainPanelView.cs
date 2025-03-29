using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelView
{
    private MainPanel obj;
    public Text t_money;
    public Text t_diamond;

    //public Button btn_start;

    public Transform item_parent;
    public List<GameObject> items = new List<GameObject>();
    public MainPanelView(IGameObject obj)
    {
        this.obj = obj as MainPanel;
    }

    internal void Initialize()
    {
        t_money = GameObject.Find("MainPanel(Clone)/goid/goid_num").GetComponent<Text>();
        t_diamond = GameObject.Find("MainPanel(Clone)/diamond/dia_num").GetComponent<Text>();
        item_parent = GameObject.Find("MainPanel(Clone)/ItemWindow(Clone)").transform;
        //btn_start = GameObject.Find("MainPanel(Clone)/t_Tap").GetComponent<Button>();
        for (int i = 0; i < item_parent.childCount; i++)
        {
            items.Add(item_parent.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 显示金币
    /// </summary>
    /// <param name="money"></param>
    public void ShowMainMoney(int money)
    {
        t_money.text = money.ToString();
    }

    /// <summary>
    /// 显示钻石
    /// </summary>
    /// <param name="diamond"></param>
    public void ShowMainDiamond(int diamond)
    {
        t_diamond.text = diamond.ToString();
    }

    /// <summary>
    /// 显示主界面Item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="money"></param>
    public void ShowMainItem(ItemData item, int money)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponent<MainPanelItem>().Init(item, money);
        }
    }
}
