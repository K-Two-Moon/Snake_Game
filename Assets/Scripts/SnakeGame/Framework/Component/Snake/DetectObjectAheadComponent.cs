

using UnityEngine;

/// <summary>
/// 检测前方对象组件
/// </summary>
public class DetectObjectAheadComponent : IComponent
{
    Snake snake;

    private LineRenderer lineRenderer;
    public DetectObjectAheadComponent(ComponentType type, IGameObject obj) : base(type, obj)
    {
        snake = obj as Snake;

        lineRenderer = obj.Obj.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));  // 使用默认着色器
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    public override void Update()
    {
            Transform head = snake.head;

            //头部发射射线
            Ray ray = new Ray(head.position, head.forward);

            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, ray.origin + ray.direction * 3f); // 例如长度为10

            RaycastHit hit;
            //撞到头部
            if (Physics.Raycast(ray, out hit, 1, 1 << 3))
            {
                Debug.Log(hit.collider.name);
                // Debug.Log(hit.collider.transform.parent.gameObject.name);

                int layer = hit.transform.gameObject.layer;
                Debug.Log("layer:" + layer);
                uint id = uint.Parse(hit.collider.transform.parent.gameObject.name);
                Snake obj = World.Instance.GetObjectById(id) as Snake;
                if (obj == null)
                {
                    Debug.Log("不是蛇对象" + obj.Obj.name + "  " + obj.GetType().Name);
                    return;
                }

                //低lv
                Snake lvLow = null;
                Debug.Log("lv:" + snake.data.lv + "  " + obj.data.lv);
                //对比谁的等级高低
                // if (snake.data.lv > obj.data.lv)
                // {
                //     lvLow = obj;
                // }
                // else
                // {
                //     lvLow = snake;
                // }
                #region 测试代码
                if (snake.Obj.name.Contains("2"))
                {
                    lvLow = obj;
                }
                if (obj.Obj.name.Contains("2"))
                {
                    lvLow = snake;
                }
                if (lvLow == null)
                {
                    return;
                }
                #endregion



                World.Instance.AddToDestoryObjectBuffer(lvLow.Id);
                Debug.Log("撞头了" + lvLow.Obj.name);


            }

            int Bodylayer = LayerMask.GetMask("SnakeBody");
            //Debug.Log("Bodylayer:" + Bodylayer);
            //撞到身体
            if (Physics.Raycast(ray, out hit, 1, Bodylayer))
            {
                Debug.Log("撞身体了" + hit.transform.name);
            }



    }
}
