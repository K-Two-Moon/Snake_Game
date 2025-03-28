using System.Collections.Generic;

public class MainPanelModel
{
    private MainPanel obj;

    public MainPanelData data;
    
    public List<ItemData> items = new List<ItemData>();//三个Item的数据

    public int speed =1;//初始速度
    public int level=1;//初始等级
    public int Upgrade=1;//初始进攻范围

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
        items.Add( new ItemData {id = 1,level = 1,sum = 0,item = data.confing.itemArray[0]} );
        items.Add( new ItemData {id = 2,level = 1,sum = 0,item = data.confing.itemArray[1]} );
        items.Add( new ItemData {id = 3,level = 1,sum = 0,item = data.confing.itemArray[2]} );
        data.money = 3000;//测试初始金币
        SetSumNum();
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

    /// <summary>
    /// 添加钻石的方法
    /// </summary>
    /// <param name="num"></param>
    public void AddDiamond(int num)
    {
        data.diamond += num;
        //发消息到c

    }

    /// <summary>
    /// 计算当前等级消耗金币
    /// </summary>
    public void SetSumNum()
    {
        foreach (var item in items)
        {
            item.sum = item.level * item.item.level_Num;
        }
    }

    /// <summary>
    /// 添加等级的方法
    /// </summary>
    public void AddLevel(int index)
    {
        if(items[index].sum<= data.money)
        {
            items[index].level++;
            data.money -= items[index].sum;
            SetSumNum();
            if(index==0)
            {
                level +=1;
                MessageManager.Broadcast(CMD.AddLevel,level);
            }
            if(index==1)
            {
                speed +=1;
                MessageManager.Broadcast(CMD.AddSpeed,speed);
            }
            if(index==2)
            {
                Upgrade +=1;
                MessageManager.Broadcast(CMD.AddUpgrade,Upgrade);
            }
        }
        else
        {
            //金币不足
        }
        MessageManager.Broadcast(CMD.ShowLevel);
    }
}

public class ItemData
{
    public int id;
    public int level;
    public int sum;
    public Item item = new Item();
}