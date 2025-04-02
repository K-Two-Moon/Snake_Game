using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameOverConfing", menuName = "配置表UI/结算面板", order = 3)]
public class GameOverConfing : IConfig
{
    public GameObject gameOverPanel;

    public Vector2 panel_pos;
}
