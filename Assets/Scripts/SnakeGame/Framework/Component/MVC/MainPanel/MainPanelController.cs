using System;
using UnityEngine;
using UnityEngine.UI;

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
        //接收网络消息
        BindNetEvent();
        //接收UI消息
        BindUIEvent();
        //第一次初始化
        UpdateItem();
        
    }

    public void BindUIEvent()
    {
        view.ShowMainMoney(model.data.money);
        view.ShowMainDiamond(model.data.diamond);
        view.btn_start.onClick.AddListener(() => 
        {
            //开始游戏
            Debug.Log("开始游戏");
        });
    }

    public void BindNetEvent()
    {
        MessageManager.AddListener(CMD.Child, (string num) =>
        {
            int inpex = int.Parse(num)-1;
            model.AddLevel(inpex);
        });
        MessageManager.AddListener(CMD.ShowLevel,()=>
        {
            UpdateItem();
            view.ShowMainMoney(model.data.money);
        });
        MessageManager.AddListener(CMD.ShowDiamond,()=>
        {
            view.ShowMainDiamond(model.data.diamond);
        });
        MessageManager.AddListener(CMD.ShowMonwy,()=>
        {
            view.ShowMainMoney(model.data.money);
        });
    }

    public override void Update()
    {
        base.Update();

    }

    public void UpdateItem()
    {
        for (int i = 0; i < model.items.Count; i++)
        {
            view.items[i].GetComponent<MainPanelItem>().Init(model.items[i],model.data.money);
        }
    }
}
