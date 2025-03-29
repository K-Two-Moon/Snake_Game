/// <summary>
/// 菜单场景物体模块
/// </summary>
public class MenuSceneObjectModule : IModule
{
    private SceneStateController controller;

    public MenuSceneObjectModule(SceneStateController controller)
    {
        this.controller = controller;
    }

    //暂时不写多线程优化
    //List<Snake> snakeList = new List<Snake>();
    public void Initialize()
    {
        // 创建主界面,然后传入数据
        MainPanel mainPanel = UIFactory.CreateProduct(UIType.MainPanel) as MainPanel;
        //传数据
        MainPanelData mainPanelData = new MainPanelData(ConfigManager.Instance.GetConfig<MainPanelConfing>() as MainPanelConfing);
        mainPanel.InitializeData(mainPanelData);
        //开始创建
        mainPanel.Create();





        Snake playerModel = GameObject3DFactory.CreateProduct(GameObject3DType.SnakePlayerModle) as Snake;
        SnakeData snakeData = new SnakeData(ConfigManager.Instance.GetSnakeConfig(1) as SnakeConfig);
        playerModel.InitializeData(snakeData);
        playerModel.Create();
    }

    public void Update(float deltaTime)
    {

    }
}
