using System.Collections.Generic;

public class MainPanelModel
{
    private MainPanel obj;

    public MainPanelData data;
    
    public List<Item> items = new List<Item>();//三个Item的数据

    public MainPanelModel(IGameObject obj)
    {
        this.obj = obj as MainPanel;
        data = this.obj.data;
    }

    /// <summary>
    /// 这里获取数据
    /// </summary>
    internal void Initialize()
    {
        items.Add(data.confing.itemArray[0]);
        items.Add(data.confing.itemArray[1]);
        items.Add(data.confing.itemArray[2]);
    }

    /// <summary>
    /// 添加金币的方法
    /// </summary>
    /// <param name="num"></param>
    public void AddMoney(int num)
    {
        data.money += num;
        //发消息到c

    }
}
