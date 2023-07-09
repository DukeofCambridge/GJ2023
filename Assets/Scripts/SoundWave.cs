using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {
        float scale = Settings.SoundScale;
        transform.DOScale(new Vector3(scale, scale, 0),1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            //Debug.Log("检测！");
            other.GetComponent<CheckPoint>()._checkList.checkNum += 1;
            other.GetComponent<CircleCollider2D>().enabled = false;
        
        }
        
    }
}
