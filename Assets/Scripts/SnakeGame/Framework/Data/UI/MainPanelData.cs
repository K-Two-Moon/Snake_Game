using UnityEngine;

public class MainPanelData : IData
{
    public GameObject mainPanel;
    public int money = 1;//金币
    public int diamond;//钻石
    public Item[] itemArray;
    public Transform parent;

    public MainPanelData(MainPanelConfing confing)
    {
        mainPanel = confing.mainPanel;
        money = confing.money;
        diamond = confing.diamond;
        itemArray = confing.itemArray;
        parent = confing.parent;
    }
}
