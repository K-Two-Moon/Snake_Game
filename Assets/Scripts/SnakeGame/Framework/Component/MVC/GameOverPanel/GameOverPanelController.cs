using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameOverPanelController : IComponent
{
    GameOverPanelmodel model;
    GameOverPanelView view;
    public GameOverPanelController(ComponentType type, IGameObject obj) : base(type, obj)
    {
        model = new GameOverPanelmodel(obj);
        model.Initialize();
        view = new GameOverPanelView(obj);
        view.Initialize();
    }

    public override void Initialize()
    {
        view.img_arrow.transform.position = view.posStart.position;
        BindUIEvent();
    }

    public void BindUIEvent()
    {
        view.t_money.text = model.data.money.ToString();
        view.t_addMoney.text = "+" + model.data.addMoney;
    }

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.5f; // 到达目标的大致时间
    private bool isMovingToEnd = true;
    public override void Update()
    {
        Vector3 targetPos = isMovingToEnd ? view.posEnd.position : view.posStart.position;
        // 平滑移动
        view.img_arrow.transform.position = Vector3.SmoothDamp(
            view.img_arrow.transform.position,
            targetPos,
            ref velocity,
            smoothTime);
        // 检查是否到达目标
        if (Vector3.Distance(view.img_arrow.transform.position, targetPos) < 20f)
        {
            isMovingToEnd = !isMovingToEnd;
        }
        base.Update();
    }
}
