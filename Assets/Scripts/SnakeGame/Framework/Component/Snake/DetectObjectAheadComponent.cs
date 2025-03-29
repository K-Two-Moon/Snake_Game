

using UnityEngine;

/// <summary>
/// 检测前方对象组件
/// </summary>
public class DetectObjectAheadComponent : IComponent
{
    Snake snake;
    public DetectObjectAheadComponent(ComponentType type, IGameObject obj) : base(type, obj)
    {
        snake = obj as Snake;
    }

    public override void Update()
    {
        Transform head = snake.head;
    
        //头部发射射线
        Ray ray = new Ray(head.position, head.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, 1 << 3 | 1 << 6))
        {
            int layer = hit.collider.transform.parent.gameObject.layer;
            uint id = uint.Parse(hit.collider.transform.parent.gameObject.name);
            Snake obj = World.Instance.GetObjectById(id) as Snake;
            if (obj == null)
            {
                Debug.Log("不是蛇对象" + obj.Obj.name + "  " + obj.GetType().Name);
                return;
            }

            //高lv
            Snake lvHigh = null;
            //低lv
            Snake lvLow = null;
            //对比谁的等级高低
            if (snake.data.lv > obj.data.lv)
            {
                lvHigh = snake;
                lvLow = obj;
            }
            else
            {
                lvHigh = obj;
                lvLow = snake;
            }


            //蛇头撞蛇头
            if (layer == 3)
            {
                lvLow.Destroy();

            }
            //蛇头撞蛇的身体
            if (layer == 6)
            {
                //
            }
        }
    }
}
