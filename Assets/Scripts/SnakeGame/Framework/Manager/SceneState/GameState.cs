using UnityEngine;

public class GameState : SceneState
{
    public GameSystemFacade facade;
    GameSceneEventModule eventModule;
    GameSceneObjectModule objectModule;
    GameSceneCommandModule commandModule;
    public GameSceneObjectModule ObjectModule => objectModule;
    public GameSceneCommandModule CommandModule => commandModule;

    public GameState(SceneStateController controller) : base(controller)
    {
        sceneName = SceneStateEnum.Game.ToString();
    }


    public override void Enter()
    {
        base.Enter();

        facade = new GameSystemFacade();

        eventModule = new GameSceneEventModule(this);
        objectModule = new GameSceneObjectModule(this);
        commandModule = new GameSceneCommandModule(this);

        facade.AddModule(eventModule);
        facade.AddModule(objectModule);
        facade.AddModule(commandModule);

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
