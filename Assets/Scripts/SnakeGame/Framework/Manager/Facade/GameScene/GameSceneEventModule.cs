using System;

public class GameSceneEventModule : IModule
{
    private GameState gameState;

    public GameSceneEventModule(GameState gameState)
    {
        this.gameState = gameState;
    }


    public void Initialize()
    {
        MessageManager.AddListener<ICommand>(CMD.AddToCommandQueue, OnStartGame);
    }

    private void OnStartGame(ICommand command)
    {
        gameState.CommandModule.AddCommand(command);
    }

    public void Destroy()
    {
        MessageManager.RemoveListener<ICommand>(CMD.AddToCommandQueue, OnStartGame);
    }


    public void Update(float deltaTime)
    {

    }

}
