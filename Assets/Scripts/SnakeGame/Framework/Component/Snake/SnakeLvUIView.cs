using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeLvUIView : IComponent
{
    Snake snake;
    GameObject t_Lv;
    Camera Camera;
    Transform canvas;

    public Text t_level;


    public SnakeLvUIView(ComponentType type, IGameObject obj) : base(type, obj)
    {
        snake = obj as Snake;
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
        canvas = GameObject.Find("Canvas").transform;
    }

    public override void Initialize()
    {
        t_Lv = GameObject.Instantiate(snake.data.config.snakeLvConfig.T_lv, canvas);
        t_level = t_Lv.GetComponent<Text>();
        base.Initialize();
    }

    public override void Destroy()
    {
        GameObject.Destroy(t_Lv);
        base.Destroy();
    }

    public override void Update()
    {
        base.Update();
        if (t_Lv != null)
        {
            t_Lv.transform.position = Camera.WorldToScreenPoint(snake.head.position);
            t_level.text = "Lv" + snake.data.lv;
        }

    }
}
