using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteorManager : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Camera mainCamera;
    public int curNum = 0;
    public int touchNum = 0;

    private void Start()
    {
        StartCoroutine(Init_NewStar());
    }

    private IEnumerator Init_NewStar()
    {
        while (true)
        {
            yield return new WaitForSeconds(Settings.ShootInterval);
            if (curNum < Settings.MaxNum)
            {
                int type = Random.Range(1, 5);
                Vector3 startPos = Return_PosByType(type);
                Vector3 endPos = Return_EndPosByStartType(type);
                GameObject newStar = Instantiate(meteorPrefab, startPos, quaternion.identity);
                newStar.GetComponent<Meteor>().meteorManager = this;
                ++curNum;
                //Debug.Log(curNum);
                Vector2 dir = (endPos - startPos).normalized;
                newStar.SetActive(true);
                newStar.GetComponent<Meteor>().Start_Move(dir);
            }
        }
    }

    private Vector2 Return_EndPosByStartType(int nowType)
    {
        int newType = nowType;
        while(newType == nowType)
        {
            newType = Random.Range(1, 5);
        }

        return Return_PosByType(newType);
    }

    /// <summary>
    /// generate a pos for shooting meteor
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private Vector2 Return_PosByType(int type)
    {
        float posX = 0;
        float posY = 0;
        // radius == half of the screen height
        float h = mainCamera.orthographicSize;
        //float w = h * mainCamera.aspect;

        if (type == 1)
        {
            posX = h*0.95f;
            posY = Random.Range(-h*0.8f, h*0.8f);
        }
        else if (type == 2)
        {
            posX = -h*0.95f;
            posY = Random.Range(-h*0.8f, h*0.8f);
        }
        else if (type == 3)
        {
            posX = Random.Range(-h*0.8f, h*0.8f);
            posY = h*0.95f;
        }
        else if (type == 4)
        {
            posX = Random.Range(-h*0.8f, h*0.8f);
            posY = -h*0.95f;
        }

        return new Vector2(posX, posY);
    }
}
