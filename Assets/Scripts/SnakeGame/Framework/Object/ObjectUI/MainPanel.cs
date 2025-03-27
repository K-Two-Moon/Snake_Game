using UnityEngine;

public class MainPanel : ObjectUI
{
    public MainPanelData data;

    public override void InitializeData(IData data)
    {
        this.data = data as MainPanelData;
    }

    public override void Create()
    {
        //在这根据数据创建对象  
        GameObject.Instantiate(data.mainPanel);
        
        base.Create();
    }

    protected override void OnCreate()
    {
        //添加组件，在组件中注册事件
        AddComponent(ComponentType.MainPanelController);
        base.OnCreate();
    }
}