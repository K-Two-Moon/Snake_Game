using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiementPanelData : IData
{
    //结算面板数据
    public SettlementConfing data;

    //传初始化数据
    public TiementPanelData(SettlementConfing data)
    {
        this.data  = data;
    }
}
