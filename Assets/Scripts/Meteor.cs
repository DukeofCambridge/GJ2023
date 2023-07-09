using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Meteor : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private TrailRenderer _trailRenderer;
    private int _hp = 0;
    //public ScreenBounds screenBounds;
    public MeteorManager meteorManager;
    public GameObject soundPrefab;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        //screenBounds = GameObject.FindWithTag("ScreenBound").GetComponent<ScreenBounds>();
    }

    public void Start_Move(Vector2 dir)
    {
        _rigidbody.velocity = dir * Random.Range(Settings.MinV, Settings.MaxV);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Gravity"))
        {
            Vector2 drawDir = (other.GetComponent<Transform>().position - transform.position).normalized;
            _rigidbody.AddForce(drawDir*Settings.GravityFactor, ForceMode2D.Force);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // enter the gravity field of a planet
        if (other.CompareTag("Gravity"))
        {
            Vector2 drawDir = (other.GetComponent<Transform>().position - transform.position).normalized;
            _rigidbody.AddForce(drawDir*GameObject.Find("GravityField").GetComponent<PlanetManager>().gravity*0.2f, ForceMode2D.Impulse);
            //other.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.5f);
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(soundPrefab, transform.position, quaternion.identity);
            StartCoroutine(Start_MeteorDead());
            meteorManager.touchNum += 1;
        }
        if (other.CompareTag("Planet"))
        {
            StartCoroutine(Start_MeteorDead());
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("ScreenBound"))
        {
            ++_hp;
            if (_hp >= Settings.WrapHp)
            {
                _hp = 0;
                Destroy(gameObject);
                meteorManager.curNum -= 1;
            }
            else
            {
                //Debug.Log("hp增加了");
                if (GetComponent<CircleCollider2D>().enabled)
                {
                    ++_hp;
                    _trailRenderer.enabled = false;
                    transform.position *= -Vector2.one;
                    StartCoroutine(PauseTrail());
                }
            }
        }
    }
    private IEnumerator PauseTrail()
    {
        yield return new WaitForSeconds(0.6f);
        _trailRenderer.enabled = true;
    }
    private IEnumerator Start_MeteorDead()
    {
        _rigidbody.velocity = Vector2.zero;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().DOFade(0, 1f);
        transform.DOScale(new Vector3(2, 2, 0), 1f);
        //Debug.Log("调用！");
        yield return new WaitForSeconds(1f);
        meteorManager.curNum -= 1;
        Destroy(gameObject);
    }

    /*private void Create_NewSoundByStarPos(Vector3 starPos)
    {
        GameObject newSound = (GameObject)Instantiate(Resources.Load("Prefab/Sound_obj"));
        newSound.transform.position = starPos;
        newSound.name = Sounds_obj.transform.childCount.ToString();
        newSound.transform.parent = Sounds_obj.transform;
    }*/
}
