using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiementPanel : ObjectUI
{
    public TiementPanelData data;
    public override void InitializeData(IData data)
    {
        this.data = data as TiementPanelData;
    }

    /// <summary>
    /// 创建对象
    /// </summary>
    public override void Create()
    {
        base.Create();
    }
}
