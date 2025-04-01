using System.Collections.Generic;
using UnityEngine;

public class GameSceneCommandModule : IModule
{
    Queue<ICommand> commandQueue;
    private GameState gameState;


    public GameSceneCommandModule(GameState gameState)
    {
        this.gameState = gameState;
    }

    public void Initialize()
    {
        commandQueue = new Queue<ICommand>();
    }

    public void Update(float deltaTime)
    {
        while (commandQueue.Count > 0)
        {
           
            ICommand command = commandQueue.Dequeue();
            command.Execute();
        }
    }
    /// <summary>
    /// 添加命令到队列中
    /// </summary>
    public void AddCommand(ICommand command)
    {
        commandQueue.Enqueue(command);
    }
}
