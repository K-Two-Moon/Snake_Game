using UnityEngine;

public class MainPanelController : IComponent
{
    MainPanelModel model;
    MainPanelView view;
    public MainPanelController(ComponentType type, IGameObject obj) : base(type, obj)
    {
        model = new MainPanelModel(obj);
        model.Initialize();
        view = new MainPanelView(obj);
        view.Initialize();
    }

    public override void Initialize()
    {
        //添加网络事件
        BindNetEvent();
        //添加UI事件
        BindUIEvent();
        //第一次初始化
        UpdateItem();

    }

    public void BindUIEvent()
    {
        view.ShowMainMoney(model.data.money);
        view.ShowMainDiamond(model.data.diamond);
    }

    public void BindNetEvent()
    {
        MessageManager.AddListener(CMD.Child, (string num) =>
        {
            int inpex = int.Parse(num) - 1;
            model.AddLevel(inpex);
        });
        MessageManager.AddListener(CMD.ShowLevel, () =>
        {
            UpdateItem();
            view.ShowMainMoney(model.data.money);
        });
        MessageManager.AddListener(CMD.ShowDiamond, () =>
        {
            view.ShowMainDiamond(model.data.diamond);
        });
        MessageManager.AddListener(CMD.ShowMonwy, () =>
        {
            model.ShowMoney();
            view.ShowMainMoney(model.data.money);
        });
        MessageManager.AddListener(CMD.UpdataPlayerLevel, () =>
        {
            //调用更新的事件
        });
        MessageManager.AddListener(CMD.UpdataMoney, AddEndMoney);
    }

    public void AddEndMoney()
    {
        //调用更新金币
        model.data.money += PlayerSneakDataSingleton.Instance.playerData.initStartlevel * 133;
        //持久化金币
        PlayerSneakDataSingleton.Instance.SetMoneyData(model.data.money);
        view.ShowMainMoney(model.data.money);
    }

    public override void Update()
    {
        base.Update();

    }

    public void UpdateItem()
    {
        for (int i = 0; i < model.items.Count; i++)
        {
            view.items[i].GetComponent<MainPanelItem>().Init(model.items[i], model.data.money);
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        MessageManager.RemoveListener(CMD.UpdataMoney, AddEndMoney);
    }
}
