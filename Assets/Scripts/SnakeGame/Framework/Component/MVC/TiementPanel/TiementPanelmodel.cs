using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiementPanelmodel
{
    private TiementPanel obj;

    public TiementPanelData data;

    public TiementPanelmodel(IGameObject obj)
    {
        this.obj = obj as TiementPanel;
        data = this.obj.data;
    }
    
    /// <summary>
    /// 这里获取数据
    /// </summary>
    internal void Initialize()
    {

    }
}
