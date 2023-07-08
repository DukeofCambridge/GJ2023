using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private float timer = 0;
    private float delayTime = 5;
    public float gravity;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        gravity = Settings.GravityFactor;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= delayTime)
        {
            gravity += 1f;
            float alpha = _spriteRenderer.color.a;
            _spriteRenderer.DOFade(alpha + 0.05f, 1f);
            timer = 0;
        }
    }

}
