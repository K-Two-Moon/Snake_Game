using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelController : IComponent
{
    GamePanelView view;
    GamePanelModel model;
    public GamePanelController(ComponentType type, IGameObject obj) : base(type, obj)
    {
        model = new GamePanelModel(obj);
        model.Initialize();
        view = new GamePanelView(obj);
        view.Initialize();
    }
}
