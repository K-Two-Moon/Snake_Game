using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiementPanelController : IComponent
{
    TiementPanelmodel model;
    TiementPanelView view;
    public TiementPanelController(ComponentType type, IGameObject obj) : base(type, obj)
    {
        model = new TiementPanelmodel(obj);
        model.Initialize();
        view = new TiementPanelView(obj);
        view.Initialize();
    }

    public override void Initialize()
    {
        
    }

    public override void Update()
    {
        base.Update();
    }
}
