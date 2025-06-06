using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneStateEnum
{
    /// <summary>
    /// 菜单界面
    /// </summary>
    Menu,
    /// <summary>
    /// 游戏界面
    /// </summary>
    Game,
    /// <summary>
    /// 游戏结束界面
    /// </summary>
    GameOver
}
public class SceneStateController
{
    // 当前场景状态
    private ISceneState currentState;

    // 存储场景状态的字典
    private Dictionary<SceneStateEnum, ISceneState> stateDict;

    // 初始化场景状态字典并添加不同场景状态
    public void Initialize()
    {
        stateDict = new Dictionary<SceneStateEnum, ISceneState>();
        stateDict.Add(SceneStateEnum.Menu, new MenuState(this));
        stateDict.Add(SceneStateEnum.Game, new GameState(this));
        stateDict.Add(SceneStateEnum.GameOver, new GameOverState(this));

        ChangeState(SceneStateEnum.Menu);
    }

    // 更改当前场景状态
    public void ChangeState(SceneStateEnum state)
    {
        if (stateDict.ContainsKey(state))
        {
            //异步 设置新的场景状态，切换场景
            //SetStateAsync(stateDict[state]);
            //不切换场景
            SetStateNoLoadScene(stateDict[state]);
        }
    }

    /// <summary>
    /// 单纯切换状态，不切换场景
    /// </summary>
    private void SetStateNoLoadScene(ISceneState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter(); 
        }
    }

    // 异步设置新的场景状态
    async private void SetStateAsync(ISceneState newState)
    {
        AsyncOperation handle = SceneManager.LoadSceneAsync(newState.SceneName, LoadSceneMode.Single);

        // 等待加载完成
        //条件判断
        while (!handle.isDone)
        {
            await Task.Yield();
        }

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter(); // 异步加载新场景
        }
    }

    // 更新当前场景状态
    public void Update()
    {
        currentState?.Update();
    }
}