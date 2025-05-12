using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverSceneInputModule:IModule
{
    private SceneStateController controller;

    public GameOverSceneInputModule(SceneStateController controller)
    {
        this.controller = controller;
    }

    public void Destroy()
    {

    }

    public void Initialize()
    {

    }

    public void Update(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 检查是否点击到了UI元素
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // 获取所有被点击的UI对象
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);
                
                // 遍历结果找到UIImage
                foreach (var result in results)
                {
                    if (result.gameObject.GetComponent<UnityEngine.UI.Image>() != null)
                    {
                        if(result.gameObject.tag == "Item")
                        {
                            // 在这里处理点击逻辑
                            controller.ChangeState(SceneStateEnum.Menu);
                            MessageManager.Broadcast(CMD.UpdataMoney);
                        }
                    }
                }
            }
        }
    }
}