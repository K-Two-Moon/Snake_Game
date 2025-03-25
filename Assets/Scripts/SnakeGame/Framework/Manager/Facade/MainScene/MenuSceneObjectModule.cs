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
        ObjectUI mainPanel = UIFactory.CreateProduct(UIType.MainPanel);
        //mainPanel.InitializeData();

    }

    public void Update(float deltaTime)
    {

    }
}
