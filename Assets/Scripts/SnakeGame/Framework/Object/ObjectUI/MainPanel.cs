public class MainPanel : ObjectUI
{
    private MainPanelData data;

    public override void InitializeData(IData data)
    {
        this.data = data as MainPanelData;
    }

    public override void Create()
    {
        base.Create();

       
        
    }

    protected override void OnCreate()
    {
        //添加组件，在组件中注册事件
        AddComponent(ComponentType.MainPanelController);
        base.OnCreate();
    }
}