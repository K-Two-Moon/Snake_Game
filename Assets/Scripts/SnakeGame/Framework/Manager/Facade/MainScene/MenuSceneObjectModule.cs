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
        MainPanelData mainPanelData = new MainPanelData(ConfigManager.Instance.GetUIConfig<MainPanelConfing>() as MainPanelConfing);
        mainPanel.InitializeData(mainPanelData);
        //开始创建
        mainPanel.Create();


    }

    public void Update(float deltaTime)
    {

    }
}
