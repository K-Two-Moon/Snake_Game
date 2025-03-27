using UnityEngine;

internal class GameState : SceneState
{
    GameSystemFacade facade;
    public GameState(SceneStateController controller) : base(controller)
    {
        sceneName = SceneStateEnum.Game.ToString();
    }

    public override void Enter()
    {
        base.Enter();

        facade = new GameSystemFacade();

        facade.AddModule(new GameSceneInputModule());
        facade.AddModule(new GameSceneObjectModule());

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
