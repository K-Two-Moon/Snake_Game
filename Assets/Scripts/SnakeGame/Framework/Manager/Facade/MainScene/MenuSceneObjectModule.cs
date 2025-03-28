/// <summary>
/// 菜单场景物体模块
/// </summary>
public class MenuSceneObjectModule : IModule
{
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



        // 这里是我的测试代码======================

        Snake player = GameObject3DFactory.CreateProduct(GameObject3DType.SnakePlayer) as Snake;
        SnakeData snakeData = new SnakeData(ConfigManager.Instance.GetSnakeConfig(1) as SnakeConfig);
        player.InitializeData(snakeData);
        player.Create();

        for (int i = 2; i <= 7; i++)
        {
            Snake enemy = GameObject3DFactory.CreateProduct(GameObject3DType.SnakeEnemy) as Snake;
            SnakeData snakeDataEnemy = new SnakeData(ConfigManager.Instance.GetSnakeConfig((uint)i) as SnakeConfig);
            enemy.InitializeData(snakeDataEnemy);
            enemy.Create();
        }
    }

    public void Update(float deltaTime)
    {

    }
}
