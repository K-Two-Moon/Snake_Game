using System;
using System.Collections.Generic;

public class GameSystemFacade
{
    List<IModule> moduleList = new List<IModule>();

    public void Initialize()
    {
        foreach (var moduled in moduleList)
        {
            moduled.Initialize();
        }
    }

    public void AddModule(IModule moduled)
    {
        moduleList.Add(moduled);
    }

    // 统一对外方法
    public void Update(float deltaTime)
    {
        foreach (var moduled in moduleList)
        {
            moduled.Update(deltaTime);
        }
    }
}
