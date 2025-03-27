using UnityEngine;

public class GameLoop : MonoBehaviour
{
    // 场景状态
    private SceneStateController m_SceneStateController;

    void Start()
    {
        // 设置帧率
        Application.targetFrameRate = 30;

        // 切换场景不被删除
        DontDestroyOnLoad(gameObject);

        ConfigManager.Instance.Initialize();

        m_SceneStateController = new SceneStateController();
        m_SceneStateController.Initialize();
    }


    void Update()
    {
        m_SceneStateController.Update();
    }
}
