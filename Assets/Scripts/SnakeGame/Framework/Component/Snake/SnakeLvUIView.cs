using UnityEngine;
using UnityEngine.UI;

public class SnakeLvUIView : IComponent
{
    Snake snake;
    GameObject t_Lv;
    Camera Camera;
    Transform canvas;

    public Text t_level;

    public Snake maxSnake;

    public SnakeLvUIView(ComponentType type, IGameObject obj) : base(type, obj)
    {
        snake = obj as Snake;
        Camera cameraObj = GameObject.Find("Camera").GetComponent<Camera>();
        if (cameraObj != null)
        {
            Camera = cameraObj.GetComponent<Camera>();
        }
        else
        {
            Debug.LogError("Camera GameObject not found in the scene.");
        }

        GameObject canvasObj = GameObject.Find("Canvas");
        if (canvasObj != null)
        {
            canvas = canvasObj.transform;
        }
        else
        {
            Debug.LogError("Canvas GameObject not found in the scene.");
        }
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
    /// <summary>
    /// 生成皇冠
    /// </summary>
    /// <returns></returns>
    public GameObject AddKing()
    {
        GameObject go = GameObject.Instantiate(snake.data.config.snakeLvConfig.kingObj);
        return go;
    }

    public override void Update()
    {
        base.Update();
        if (t_Lv != null)
        {
            // if (snake == null || snake.data == null || snake.head || t_Lv.transform == null)
            // {
            //     Debug.Log(2);
            //     return;
            // }
            try
            {
                t_Lv.transform.position = Camera.WorldToScreenPoint(snake.head.position);
                t_level.text = "Lv" + snake.data.lv;
            }
            catch
            {

             
            }

        }
        maxSnake = World.Instance.maxSnake;
        // if(maxSnake.Id != snake.Id)
        // {
        //     GameObject.Destroy(World.Instance.king);
        // }
    }
}
