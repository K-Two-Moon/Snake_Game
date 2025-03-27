using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager:Singleton<ConfigManager>
{
    Dictionary<string,IConfig> dict = new Dictionary<string, IConfig>();
    public void Initialize()
    {
        MainPanelConfing mainPanelConfing = Resources.Load<MainPanelConfing>("UIConfig/MainConfig");
        dict.Add(typeof(MainPanelConfing).Name,mainPanelConfing);
    }

    public void Dispose()
    {
       
        dict = null;
    }

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
}
