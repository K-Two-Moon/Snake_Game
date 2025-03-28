using Cinemachine;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 菜单场景物体模块
/// </summary>
public class MenuSceneObjectModule : IModule
{
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

        IGameObject player = GameObject3DFactory.CreateProduct(GameObject3DType.SnakePlayer);
    }

    public void Update(float deltaTime)
    {

    }
}
