using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ScreenWrap : MonoBehaviour
{
    private TrailRenderer _trailRenderer;
    private void Awake()
    {
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ScreenBound"))
        {
            _trailRenderer.enabled = false;
            transform.position *= -Vector2.one;
            StartCoroutine(PauseTrail());
            
        }
    }
    private IEnumerator PauseTrail()
    {
        yield return new WaitForSeconds(0.6f);
        _trailRenderer.enabled = true;
    }
}
