using UnityEngine;

public class GameOverState : SceneState
{
    /// <summary>
    /// ���ģʽ
    /// </summary>
    GameSystemFacade facade;
    public GameOverState(SceneStateController controller) : base(controller)
    {
        sceneName = SceneStateEnum.GameOver.ToString();
    }

    public override void Enter()
    {
        //�����г�ʼ������
        base.Enter();

        facade = new GameSystemFacade();

        facade.AddModule(new GameOverSceneInputModule(controller));
        facade.AddModule(new GameOverSceneObjectModule(controller));

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

        //���ݸ����ˣ������ٸ���
        base.Update();
    }
}