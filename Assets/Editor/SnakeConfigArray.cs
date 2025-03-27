using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SnakeArray", menuName = "配置表3D/SnakeArray", order = 0)]
public sealed class SnakeConfigArray : IConfig
{
    public SnakeConfig[] configsArray;

    private void OnValidate()
    {
        for (uint i = 0; i < configsArray.Length; i++)
        {
            configsArray[i].id = i + 1;
        }

        foreach (var config in configsArray)
        {
            config.head = Resources.Load<GameObject>("SnakeMesh/Head/Boss_head" + config.id);
            config.body = Resources.Load<GameObject>("SnakeMesh/Body/Boss_body" + config.id);
            config.tail = Resources.Load<GameObject>("SnakeMesh/Tail/Boss_tail" + config.id);
        }
    }
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