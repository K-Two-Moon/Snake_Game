using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : ObjectUI
{
    public GameOverPanelData data;
    public override void InitializeData(IData data)
    {
        this.data = data as GameOverPanelData;
    }

    /// <summary>
    /// 创建对象
    /// </summary>
    public override void Create()
    {
        if (obj == null)
        {
            obj = GameObject.Instantiate(data.config.gameOverPanel);
            obj.transform.position = data.config.panel_pos;
        }

        base.Create();
    }

    protected override void OnCreate()
    {
        AddComponent(ComponentType.GameOverController);
        base.OnCreate();
    }
}
