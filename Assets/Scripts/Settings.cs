using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Settings
{
    // Player Settings
    [Header("初始速度")] public const float InitialSpeed = 2f;
    [Header("加速度因子")] public const float AccFactor = 10f;
    [Header("转向因子")] public const float TurnFactor = 3.5f;
    [Header("漂移因子")] public const float DriftFactor = 0.95f;  // decide how fast the lateral velocity will decrease
    [Header("最大速度")] public const float MaxSpeed = 12f;
    [Header("最小速度")] public const float MinSpeed = 4f;
    
    // Planet Settings
    [Header("引力因子")] public const float GravityFactor = 4f;
    
    // Meteor Settings
    [Header("刷新间隔")] public const float ShootInterval = 2.5f;
    [Header("最大流星数量")] public const int MaxNum = 3;
    [Header("流星最小速度")] public const float MinV = 2f;
    [Header("流星最大速度")] public const float MaxV = 6f;
    [Header("流星最多跨越边界次数")] public const int WrapHp = 3;
    
    // Game Settings
    [Header("扫帚插星球死亡计时")] public const float DieTime = 5f;
    [Header("音波扩散大小(决定游戏难度)")] public const float SoundScale = 5f;
    [Header("引力强度提升1所需秒数")] public const float EnhanceInterval = 10000f;
}
