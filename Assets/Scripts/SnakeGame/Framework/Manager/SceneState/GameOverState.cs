internal class GameOverState : SceneState
{
    public GameOverState(SceneStateController controller) : base(controller)
    {
        sceneName = SceneStateEnum.GameOver.ToString();
    }
}