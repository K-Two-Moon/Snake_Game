public class MainPanelModel
{
    private MainPanel obj;

    public MainPanelData data;

    public MainPanelModel(IGameObject obj)
    {
        this.obj = obj as MainPanel;
        this.data = this.obj.data as MainPanelData;
    }

    /// <summary>
    /// 这里获取数据
    /// </summary>
    internal void Initialize()
    {

    }
}