using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController[] playerControllers;
    public MeteorManager meteorManager;
    public GameObject start;
    public GameObject Button;
    public GameObject Square;
    public GameObject home;
    public CheckList checkList;
    public GameObject gameOver;
    public GameObject victory;

    public GameObject level1Prefab;
    public GameObject level1Instance;
    public void Begin()
    {
        level1Instance = Instantiate(level1Prefab, Vector3.zero, quaternion.identity);
        playerControllers[0] = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerControllers[1] = GameObject.Find("Player2").GetComponent<PlayerController>();
        meteorManager = GameObject.Find("Meteors Manager").GetComponent<MeteorManager>();
        checkList = GameObject.Find("CheckList").GetComponent<CheckList>();
        StartCoroutine(Go());
    }

    public void GoHome()
    {
        StartCoroutine(Home());
    }

    public void GameOver()
    {
        StartCoroutine(Over());
    }

    public void FinalVictory()
    {
        StartCoroutine(Vic());
    }

    private IEnumerator Go()
    {
        start.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);
        start.SetActive(false);
        Button.transform.DOMoveY(16f, 2f);
        yield return new WaitForSeconds(2f);
        Button.SetActive(false);
        
        
        Square.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);
        home.SetActive(true);
        Square.SetActive(false);
        
        foreach (var player in playerControllers)
        {
            player.Run();
        }
        meteorManager.Run();
    }

    private IEnumerator Home()
    {
        Square.GetComponent<SpriteRenderer>().color = new Color(0, 0 ,0, 0);
        Square.SetActive(true);
        Square.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
        home.SetActive(false);
        
        yield return new WaitForSeconds(2f);
        Destroy(level1Instance);
        GameObject[] ms = GameObject.FindGameObjectsWithTag("Meteor");
        foreach (var m in ms)
        {
            Destroy(m);
        }
        GameObject[] waves = GameObject.FindGameObjectsWithTag("SoundWave");
        foreach (var wave in waves)
        {
            Destroy(wave);
        }
        
        Button.SetActive(true);
        Button.transform.DOMoveY(-1f, 2f);
        
        yield return new WaitForSeconds(2f);
        start.SetActive(true);
        start.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator Over()
    {
        gameOver.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        gameOver.SetActive(true);
        gameOver.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);
        Destroy(level1Instance);
        Square.GetComponent<SpriteRenderer>().color = new Color(0, 0 ,0, 0);
        Square.SetActive(true);
        Square.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
        home.SetActive(false);
        
        yield return new WaitForSeconds(2f);
        
        GameObject[] ms = GameObject.FindGameObjectsWithTag("Meteor");
        foreach (var m in ms)
        {
            Destroy(m);
        }
        GameObject[] waves = GameObject.FindGameObjectsWithTag("SoundWave");
        foreach (var wave in waves)
        {
            Destroy(wave);
        }
        
        Button.SetActive(true);
        Button.transform.DOMoveY(-1f, 2f);
        
        yield return new WaitForSeconds(2f);
        start.SetActive(true);
        start.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);
        gameOver.SetActive(false);
        
    }

    private IEnumerator OverWrap()
    {
        StartCoroutine(Over());
        yield return new WaitForSeconds(2f);
        StartCoroutine(Home());
        gameOver.SetActive(false);
    }

    private IEnumerator Vic()
    {
        victory.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        victory.SetActive(true);
        victory.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);
        Destroy(level1Instance);
        Square.GetComponent<SpriteRenderer>().color = new Color(0, 0 ,0, 0);
        Square.SetActive(true);
        Square.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
        home.SetActive(false);
        
        yield return new WaitForSeconds(2f);

        GameObject[] ms = GameObject.FindGameObjectsWithTag("Meteor");
        foreach (var m in ms)
        {
            Destroy(m);
        }
        GameObject[] waves = GameObject.FindGameObjectsWithTag("SoundWave");
        foreach (var wave in waves)
        {
            Destroy(wave);
        }
        
        Button.SetActive(true);
        Button.transform.DOMoveY(-1f, 2f);
        
        yield return new WaitForSeconds(2f);
        start.SetActive(true);
        start.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);
        victory.SetActive(false);
        
    }
    private IEnumerator VicWrap()
    {
        StartCoroutine(Vic());
        yield return new WaitForSeconds(2f);
        StartCoroutine(Home());
        victory.SetActive(false);
    }
}
