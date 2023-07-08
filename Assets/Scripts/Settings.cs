using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Settings
{
    
    [Header("初始速度")]
    public const float InitialSpeed = 2f;
    [Header("加速度因子")]
    // acceleration factor
    public const float Acc = 3f;
    [Header("转向因子")]
    // turn factor
    public const float Turn = 3.5f;

}
