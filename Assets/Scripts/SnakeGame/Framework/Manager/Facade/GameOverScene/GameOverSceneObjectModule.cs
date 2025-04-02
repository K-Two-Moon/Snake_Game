public class GameOverSceneObjectModule:IModule
{
    private SceneStateController controller;

    public GameOverSceneObjectModule(SceneStateController controller)
    {
        this.controller = controller;
    }

    public void Initialize()
    {
        // 创建主界面,然后传入数据
        GameOverPanel gameOverPanel = UIFactory.CreateProduct(UIType.GameOverPanel) as GameOverPanel;
        //传数据
        GameOverPanelData gameOverPanelData = new GameOverPanelData(ConfigManager.Instance.GetConfig<GameOverConfing>() as GameOverConfing);
        gameOverPanel.InitializeData(gameOverPanelData);
        //开始创建
        gameOverPanel.Create();
    }

    public void Update(float deltaTime)
    {

    }
}