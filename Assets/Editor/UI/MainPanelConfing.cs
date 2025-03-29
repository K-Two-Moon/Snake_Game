using System;
using UnityEngine;

//1 默认名

//2.菜单名

//3.索引
[CreateAssetMenu(fileName = "MainConfig", menuName = "配置表UI/main", order = 0)]
public class MainPanelConfing : IConfig
{
    public GameObject mainPanel;
    public Vector2 panel_pos;
    public Vector2 size;
    public Item[] itemArray;
    public Transform parent;
    [Header("窗口位置")]
    public Vector2 parent_pos;

    private void OnValidate()
    {
        //foreach (var item in itemArray)
        //{
        //    item.sum = item.level * item.level_Num;
        //}
        mainPanel = Resources.Load<GameObject>("UIPrefab/MainPanel");
        panel_pos = new Vector2(720, 1544);
        size = new Vector2(0.6f, 0.6f);
        itemArray[0] = new Item();
        itemArray[0].itemObj = Resources.Load<GameObject>("UIPrefab/Item1");

        itemArray[0].bg_true = Resources.Load<Sprite>("Texture2D/BTN_main");
        itemArray[0].bg_false = Resources.Load<Sprite>("Texture2D/BTN_main_2");

        itemArray[0].down_icon_true = Resources.Load<Sprite>("Texture2D/ICN_Money");
        itemArray[0].down_icon_false = Resources.Load<Sprite>("Texture2D/ICN_ad");

        itemArray[0].level_Num = 800;

        itemArray[1] = new Item();
        itemArray[1].itemObj = Resources.Load<GameObject>("UIPrefab/Item2");

        itemArray[1].bg_true = Resources.Load<Sprite>("Texture2D/BTN_main");
        itemArray[1].bg_false = Resources.Load<Sprite>("Texture2D/BTN_main_2");

        itemArray[1].down_icon_true = Resources.Load<Sprite>("Texture2D/ICN_Money");
        itemArray[1].down_icon_false = Resources.Load<Sprite>("Texture2D/ICN_ad");

        itemArray[1].level_Num = 800;

        itemArray[2] = new Item();
        itemArray[2].itemObj = Resources.Load<GameObject>("UIPrefab/Item3");

        itemArray[2].bg_true = Resources.Load<Sprite>("Texture2D/BTN_main");
        itemArray[2].bg_false = Resources.Load<Sprite>("Texture2D/BTN_main_2");

        itemArray[2].down_icon_true = Resources.Load<Sprite>("Texture2D/ICN_Money");
        itemArray[2].down_icon_false = Resources.Load<Sprite>("Texture2D/ICN_ad");

        itemArray[2].level_Num = 800;

        parent = Resources.Load<GameObject>("UIPrefab/ItemWindow").transform;

        parent_pos = new Vector2(0,-950);
    }
}

[Serializable]
public class Item
{
    public GameObject itemObj;
    /// <summary>
    /// 每级需要多少金币的差值
    /// </summary>
    public int level_Num;
    public Sprite bg_true;
    public Sprite bg_false;
    public Sprite down_icon_true;
    public Sprite down_icon_false;
}