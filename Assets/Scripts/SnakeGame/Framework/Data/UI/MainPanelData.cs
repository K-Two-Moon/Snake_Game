using log4net.Core;
using UnityEngine;

public class MainPanelData : IData
{
    /// <summary>
    /// 不变的
    /// </summary>
    public MainPanelConfing confing;

    /// <summary>
    /// 变的
    /// </summary>
    public int money;//金币
    public int diamond;//钻石

    public MainPanelData(MainPanelConfing confing)
    {
        this.confing = confing;
    }
}
