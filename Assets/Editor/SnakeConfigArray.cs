using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SnakeArray", menuName = "配置表3D/SnakeArray", order = 0)]
public sealed class SnakeConfigArray : IConfig
{
    public SnakeConfig[] configsArray;
}

[Serializable]
public class SnakeConfig
{
    public uint id;
    /// <summary>
    /// 蛇的头
    /// </summary>
    public GameObject head;
    /// <summary>
    /// 蛇的身体
    /// </summary>
    public GameObject body;
    /// <summary>
    /// 蛇的尾巴
    /// </summary>
    public GameObject tail;
}