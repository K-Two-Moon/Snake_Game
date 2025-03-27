using System.Collections.Generic;
using UnityEngine;

public class MainPanel : ObjectUI
{
    public MainPanelData data;
    public Transform itemWindow;
    public List<GameObject> list = new List<GameObject>();

    public override void InitializeData(IData data)
    {
        this.data = data as MainPanelData;
    }

    public override void Create()
    {
        //在这根据数据创建对象  
        obj = GameObject.Instantiate(data.confing.mainPanel);
        obj.transform.localPosition = data.confing.panel_pos;
        //这是Item窗口
        itemWindow = GameObject.Instantiate(data.confing.parent);
        itemWindow.parent = obj.transform;
        itemWindow.localPosition = data.confing.parent_pos;
        //这是三个Item对象
        for (int i = 0; i < 3; i++)
        {
            GameObject go = GameObject.Instantiate(data.confing.itemArray[i].itemObj);
            list.Add(go);
            go.transform.parent = itemWindow;
            //添加对应的Item脚本
            go.AddComponent<MainPanelItem>();
        }

        base.Create();
    }

    protected override void OnCreate()
    {
        //添加组件，在组件中注册事件
        AddComponent(ComponentType.MainPanelController);
        base.OnCreate();
    }
}