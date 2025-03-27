using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>
{
    /// <summary>
    /// ScriptObject配置文件
    /// </summary>
    Dictionary<string, IConfig> dict = new Dictionary<string, IConfig>();

    /// <summary>
    /// 蛇配置文件
    /// </summary>
    public Dictionary<uint, SnakeConfig> snakeConfigDict = new Dictionary<uint, SnakeConfig>();

    public void Initialize()
    {
        // 加载UI配置文件
        MainPanelConfing mainPanelConfing = Resources.Load<MainPanelConfing>("UIConfig/MainConfig");
        dict.Add(typeof(MainPanelConfing).Name, mainPanelConfing);

        // 加载蛇配置文件
        SnakeConfigArray snakeConfigArray = Resources.Load<SnakeConfigArray>("3DConfig/SnakeArray");
        dict.Add(typeof(SnakeConfigArray).Name, snakeConfigArray);
        foreach (SnakeConfig config in snakeConfigArray.configsArray)
        {
            snakeConfigDict.Add(config.id, config);
        }
    }

    public void Dispose()
    {
        dict.Clear();
        dict = null;
    }

    /// <summary>
    /// 获取ScriptObject配置文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IConfig GetConfig<T>()
    {
        string name = typeof(T).Name;
        if (dict.ContainsKey(name))
        {
            return dict[name];
        }
        else
        {
            Debug.LogError("没有这个数据");
            return null;
        }
    }

    /// <summary>
    /// 获取蛇配置文件
    /// </summary>
    public SnakeConfig GetSnakeConfig(uint id)
    {
        if (snakeConfigDict.ContainsKey(id))
        {
            return snakeConfigDict[id];
        }
        else
        {
            Debug.LogError("没有这个数据");
            return null;
        }
    }
}
