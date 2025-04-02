using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : ObjectUI
{
    GamePanelData data;
    public override void InitializeData(IData data)
    {
        
    }

    /// <summary>
    /// 创建面板
    /// </summary>
    public override void Create()
    {
        base.Create();
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    protected override void OnCreate()
    {
        AddComponent(ComponentType.GameController); 
        base.OnCreate();
    }
}
