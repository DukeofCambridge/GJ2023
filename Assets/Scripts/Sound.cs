using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private List<GameObject> Effects = new List<GameObject>();

    private int NowId = 0;
    private bool IsWait = false;
    private bool IsFirst = true;
    private void Start()
    {
        foreach(var child in this.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Effects.Add(child.gameObject);
        }
    }

    private void Update()
    {
        if (!IsWait)
        {
            IsWait = true;
            if (IsFirst)
            {
                IsFirst = false;
                StartCoroutine(Begin_WaitTime(0));
            }
            else
            {
                StartCoroutine(Begin_WaitTime());
            }
        }
    }

    private IEnumerator Begin_WaitTime(float waitTime = 1f)
    {
        yield return new WaitForSeconds(waitTime);
       
        if (Effects.Count > 0)
        {
            StartCoroutine(Start_ChangeScale(Effects[NowId].transform));
            StartCoroutine(Start_ChangeColor(Effects[NowId].transform));
            
            NowId++;
            if (NowId == Effects.Count)
            {
                NowId = 0;
            }
        }
        IsWait = false;
    }

    private IEnumerator Start_ChangeScale(Transform child)
    {
        child.gameObject.SetActive(true);
        child.localScale = new Vector3(0, 0, 0);
     
        float sprite_Scale = 0;
        while (true)
        {
            if (sprite_Scale < 4)
            {
                sprite_Scale += Time.deltaTime * 2;
                child.localScale = new Vector3(sprite_Scale, sprite_Scale, 1);
            }
            else
            {
                child.gameObject.SetActive(false);
                break;
            }
            yield return 1;
        }
    }

    private IEnumerator Start_ChangeColor(Transform child)
    {
        child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    
        float sprite_Color = 1f;
        while (true)
        {
            if (sprite_Color > 0f)
            {
                sprite_Color -= Time.deltaTime / 2;
                child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, sprite_Color);
            }
            else
            {           
                break;
            }
            yield return 1;
        }
    }
}
