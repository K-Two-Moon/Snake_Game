using UnityEngine;

internal class MenuState : SceneState
{
    /// <summary>
    /// 外观模式
    /// </summary>
    GameSystemFacade facade;
    public MenuState(SceneStateController controller) : base(controller)
    {
        sceneName = SceneStateEnum.Menu.ToString();
    }

    public override void Enter()
    {
        //基类中初始化世界
        base.Enter();

        facade = new GameSystemFacade();

        facade.AddModule(new MenuSceneInputModule(controller));
        facade.AddModule(new MenuSceneObjectModule(controller));

        facade.Initialize();
    }

    public override void Exit()
    {
        facade = null;
        base.Exit();
    }

    public override void Update()
    {
        facade.Update(Time.deltaTime);

        //数据更新了，世界再更新
        base.Update();
    }
}
