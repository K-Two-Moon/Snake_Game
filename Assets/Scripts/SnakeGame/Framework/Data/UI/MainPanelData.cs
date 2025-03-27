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
    public int money = 1;//金币
    public int diamond;//钻石

    public int level1;
    public int level2;
    public int level3;

    public int sum1;
    public int sum2;
    public int sum3;

    public MainPanelData(MainPanelConfing confing)
    {
        this.confing = confing;
    }
}
