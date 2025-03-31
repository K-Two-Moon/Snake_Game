using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
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
        // 加载UI主面板配置文件
        MainPanelConfing mainPanelConfing = Resources.Load<MainPanelConfing>("UIConfig/MainConfig");
        dict.Add(typeof(MainPanelConfing).Name, mainPanelConfing);

        // 加载头顶等级
        SneakLvUIViewConfig sneakLvUIViewConfig = Resources.Load<SneakLvUIViewConfig>("UIConfig/SneakLvUIViewConfig");
        dict.Add(typeof(SneakLvUIViewConfig).Name, sneakLvUIViewConfig);

        // 加载蛇配置文件
        SnakeConfigArray snakeConfigArray = Resources.Load<SnakeConfigArray>("3DConfig/SnakeArray");
        dict.Add(typeof(SnakeConfigArray).Name, snakeConfigArray);
        foreach (SnakeConfig config in snakeConfigArray.configsArray)
        {
            snakeConfigDict.Add(config.id, config);
        }
    }

    public void SetPlayerSneakData()
    {
        string jsonPlayer = JsonConvert.SerializeObject(PlayerSneakDataSingleton.Instance.playerData, Formatting.Indented);
        //string filePath = Path.Combine(Application.persistentDataPath, "playerInfo.json");
        //测试路径
        string filePath = Path.Combine(Application.dataPath + "/Resources/", "playerInfo.json");
        Debug.Log(filePath);
        File.WriteAllText(filePath, jsonPlayer);
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 主面板数据持久（金币、钻石）
    /// </summary>
    public void SetMainData()
    {
        string jsonPlayer = JsonConvert.SerializeObject(PlayerSneakDataSingleton.Instance.playerData, Formatting.Indented);
        //string filePath = Path.Combine(Application.persistentDataPath, "playerInfo.json");
        //测试路径
        string filePath = Path.Combine(Application.dataPath + "/Resources/", "main.json");
        Debug.Log(filePath);
        File.WriteAllText(filePath, jsonPlayer);
        AssetDatabase.Refresh();
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
