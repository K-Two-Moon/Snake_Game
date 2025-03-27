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
        view.t_money.text = model.data.money.ToString(); 
        view.t_diamond.text = model.data.diamond.ToString();
        view.btn_start.onClick.AddListener(() =>
        {
            //开始游戏
            Debug.Log("开始游戏");
        });
        UpdateItem();
    }

    public override void Update()
    {
        base.Update();

    }

    public void UpdateItem()
    {
        if (model.data.money >= model.data.sum1)
        {
            view.items[0].GetComponent<MainPanelItem>().Init(true, model.items[0], model.data.level1);
        }
        else
        {
            view.items[0].GetComponent<MainPanelItem>().Init(false, model.items[0], model.data.level1);
        }
        if (model.data.money >= model.data.sum1)
        {
            view.items[1].GetComponent<MainPanelItem>().Init(true, model.items[1], model.data.level2);
        }
        else
        {
            view.items[1].GetComponent<MainPanelItem>().Init(false, model.items[1], model.data.level2);
        }
        if (model.data.money >= model.data.sum1)
        {
            view.items[2].GetComponent<MainPanelItem>().Init(true, model.items[2], model.data.level3);
        }
        else
        {
            view.items[2].GetComponent<MainPanelItem>().Init(false, model.items[2], model.data.level3);
        }
    }
}
