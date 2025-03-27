using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IConfig:ScriptableObject
{

}

//1 默认名

//2.菜单名

//3.索引
[CreateAssetMenu(fileName = "main", menuName = "配置表/main", order = 0)]
public class MainPanelConfing : IConfig
{
    public GameObject mainPanel;
    public int money = 1;//金币
    public int diamond;//钻石
    public  Item[] itemArray;
    public Transform parent;
    [Header("窗口位置")]
    public Vector2 parent_pos;
    private void OnValidate()
    {
        foreach (var item in itemArray)
        {
            item.sum = item.level * item.level_Num;
        }
    }
}

[Serializable]
public class Item
{
    public GameObject itemObj;
    public int level;
    public int level_Num;
    public int sum;
    public Sprite bg_true;
    public Sprite bg_false;
    //public Sprite icon;
    //public string t_name;
    //public int level_num;
    public Sprite down_icon_true;
    public Sprite down_icon_false;
    public int money_num;
}