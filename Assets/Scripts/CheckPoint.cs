using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public CheckList _checkList;

    private void Awake()
    {
        _checkList = GetComponentInParent<CheckList>();
    }

    /*private void OnTriggerStay2D(Collider other)
    {
        if (other.CompareTag("SoundWave"))
        {
            Debug.Log("检测！");
            _checkList.checkNum += 1;
            GetComponent<CircleCollider2D>().enabled = false;
            
        }
    }*/
}
