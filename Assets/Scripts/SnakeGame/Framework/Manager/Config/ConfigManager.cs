using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>
{
    Dictionary<string, IConfig> uiDict = new Dictionary<string, IConfig>();
    public void Initialize()
    {
        MainPanelConfing mainPanelConfing = Resources.Load<MainPanelConfing>("MainConfig");
        uiDict.Add(typeof(MainPanelConfing).Name, mainPanelConfing);
    }

    public void Dispose()
    {
        uiDict.Clear();
        uiDict = null;
    }

    public IConfig GetUIConfig<T>()
    {
        string name = typeof(T).Name;
        if (uiDict.ContainsKey(name))
        {
            return uiDict[name];
        }
        else
        {
            Debug.LogError("没有这个数据");
            return null;
        }
    }
}
