using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSceneInputModule : IModule
{
    private SceneStateController controller;

    public MenuSceneInputModule(SceneStateController controller)
    {
        this.controller = controller;
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
                            MessageManager.Broadcast(CMD.Child, result.gameObject.name);
                        }
                        if (result.gameObject.tag == "SceneState")
                        {
                            controller.ChangeState(SceneStateEnum.Game);
                            Debug.Log("111");
                        }
                    }
                }
            }
        }
    }
}
