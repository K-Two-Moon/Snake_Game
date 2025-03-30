

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
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1, 1 << 3 | 1 << 6))
        {
            // Debug.Log(hit.collider.name);
            // Debug.Log(hit.collider.transform.parent.gameObject.name);

            int layer = hit.transform.gameObject.layer;
            // Debug.Log("layer:" + layer);
            uint id = uint.Parse(hit.collider.transform.parent.gameObject.name);
            Snake obj = World.Instance.GetObjectById(id) as Snake;
            if (obj == null)
            {
                Debug.Log("不是蛇对象" + obj.Obj.name + "  " + obj.GetType().Name);
                return;
            }

            //低lv
            Snake lvLow = null;
            Debug.Log("lv:"+ snake.data.lv + "  " + obj.data.lv);
            //对比谁的等级高低
            if (snake.data.lv > obj.data.lv)
            {
                lvLow = obj;
            }
            else
            {
                lvLow = snake;
            }


            //蛇头撞蛇头
            if (layer == 3)
            {
                //World.Instance.AddToDestoryObjectBuffer(lvLow.Id);
                Debug.Log("撞头了" + lvLow.Obj.name);
            }
            //蛇头撞蛇的身体
            if (layer == 6)
            {
                Debug.Log("撞身体了" + lvLow.Obj.name);
            }
        }
    }
}
