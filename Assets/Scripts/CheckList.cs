using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckList : MonoBehaviour
{
    public CheckPoint[] checkPoints;
    public int checkNum = 0;
    private bool ended = false;

    private void Awake()
    {
        checkPoints = GetComponentsInChildren<CheckPoint>();
    }

    private void Update()
    {
        if (checkPoints.Length == checkNum&& !ended)
        {
            Debug.Log("VICTORY");
            GameObject.Find("GameManager").GetComponent<GameManager>().FinalVictory();
            ended = true;
        }
    }

}
