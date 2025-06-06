using UnityEngine;

public enum UIType
{
    /// <summary>
    /// 主面板
    /// </summary>
    MainPanel,
    /// <summary>
    /// 结算面板
    /// </summary>
    GameOverPanel
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
        UIType.GameOverPanel => new GameOverPanel(),
        _ => null,
    };
}

