using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelView
{
    public GameOverPanel tiementPanel;

    public Text t_money;

    public Text t_addMoney;

    public List<GameObject> itemList = new List<GameObject>();

    public Transform posStart, posEnd;
    public Image img_arrow;

    public GameOverPanelView(IGameObject obj)
    {
        tiementPanel = obj as GameOverPanel;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        t_money = GameObject.Find("GameOverPanel(Clone)/gold/t_gold").GetComponent<Text>();

        t_addMoney = GameObject.Find("GameOverPanel(Clone)/bg_rew/t_addGold").GetComponent<Text>();
        for (int i = 0; i < 5; i++)
        {
            itemList.Add(GameObject.Find("GameOverPanel(Clone)/multiple_bg/t_item" + i).GetComponent<GameObject>());
        }
        posStart = GameObject.Find("GameOverPanel(Clone)/posStart").transform;
        posEnd = GameObject.Find("GameOverPanel(Clone)/posEnd").transform;
        img_arrow = GameObject.Find("GameOverPanel(Clone)/img_arraw").GetComponent<Image>();
    }
}
