using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Settings
{
    
    [Header("初始速度")] public const float InitialSpeed = 2f;
    [Header("加速度因子")] public const float AccFactor = 10f;
    [Header("转向因子")] public const float TurnFactor = 3.5f;
    [Header("漂移因子")] public const float DriftFactor = 0.95f;  // decide how fast the lateral velocity will decrease
    [Header("最大速度")] public const float MaxSpeed = 20f;
    [Header("最小速度")] public const float MinSpeed = 2f;
    [Header("引力因子")] public const float GravityFactor = 5f;
}
