using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneObjectModule : IModule
{
    public void Initialize()
    {
        // 这里是我的测试代码======================

        Snake player = GameObject3DFactory.CreateProduct(GameObject3DType.SnakePlayer) as Snake;
        SnakeData snakeData = new SnakeData(ConfigManager.Instance.GetSnakeConfig(1) as SnakeConfig);
        player.InitializeData(snakeData);
        player.Create();

        for (int i = 2; i <= 7; i++)
        {
            Snake enemy = GameObject3DFactory.CreateProduct(GameObject3DType.SnakeEnemy) as Snake;
            SnakeData snakeDataEnemy = new SnakeData(ConfigManager.Instance.GetSnakeConfig((uint)i) as SnakeConfig);
            enemy.InitializeData(snakeDataEnemy);
            enemy.Create();
        }
    }

    public void Update(float deltaTime)
    {

    }
}
