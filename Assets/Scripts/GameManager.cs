using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController[] playerControllers;
    public MeteorManager meteorManager;

    public void Begin()
    {
        StartCoroutine(Go());
    }

    private IEnumerator Go()
    {
        GameObject.Find("start").GetComponent<SpriteRenderer>().DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);
        GameObject.Find("start").SetActive(false);
        GameObject.Find("Button").transform.DOMoveY(16f, 2f);
        yield return new WaitForSeconds(2f);
        GameObject.Find("Button").SetActive(false);
        GameObject.Find("Square").GetComponent<SpriteRenderer>().DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);
        GameObject.Find("Square").SetActive(false);
        foreach (var player in playerControllers)
        {
            player.Run();
        }
        meteorManager.Run();
    }
    
    
}
