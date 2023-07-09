using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckList : MonoBehaviour
{
    public CheckPoint[] checkPoints;
    public int checkNum = 0;

    private void Awake()
    {
        checkPoints = GetComponentsInChildren<CheckPoint>();
    }

    private void Update()
    {
        if (checkPoints.Length == checkNum)
        {
            Debug.Log("VICTORY");
        }
    }

}
