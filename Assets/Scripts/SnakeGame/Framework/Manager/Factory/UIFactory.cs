using UnityEngine;

public enum UIType
{
    MainPanel
}


/// <summary>
/// UI工厂
/// </summary>
public static class UIFactory
{
    public static ObjectUI CreateProduct(UIType type)
    {
        ObjectUI product = null;
        product = SwitchBuilder(type);

        if (product == null)
            Debug.LogError("创建失败");

        // 返回产品
        return product;
    }

    private static ObjectUI SwitchBuilder(UIType type) => type switch
    {
        UIType.MainPanel => new MainPanel(),
        _ => null,
    };
}

