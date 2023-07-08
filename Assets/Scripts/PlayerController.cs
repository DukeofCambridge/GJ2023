using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float _accInput;
    private float _steeringInput;
    private float _rotationAngle;
    private float _velocityForward;
    public ScreenBounds screenBounds;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        _rigidbody2D.velocity = transform.up * Settings.MinSpeed;
    }

    private void Update()
    {
        // screen wrap effects
        if (screenBounds.AmIOutOfBounds(transform.position))
        {
            _trailRenderer.enabled = false;
            Vector2 newPosition = screenBounds.CalculateWrappedPosition(transform.position);
            transform.position = newPosition;
            StartCoroutine(PauseTrail());
        }
        if (GetComponent<SpriteRenderer>().color.a < 0.1f)
        {
            Debug.Log("GAME OVER");
        }
    }

    private void FixedUpdate()
    {
        // acceleration
        ApplyAccForce();
        // slow the lateral velocity to realize drift effects
        KillOrthogonalVelocity();
        // steer the direction
        ApplySteeringForce();
        // judge if the player stops
        SlowDie();
    }

    void ApplyAccForce()
    {
        // limit the range of the forward velocity of player
        _velocityForward = Vector2.Dot(_rigidbody2D.velocity, transform.up);
        if (_velocityForward > Settings.MaxSpeed && _accInput > 0)
            return;
        if (_velocityForward < Settings.MinSpeed && _accInput < 0)
            return;
        // limit the range of overall velocity of player
        if (_rigidbody2D.velocity.sqrMagnitude > Mathf.Pow(Settings.MaxSpeed,2) && _accInput > 0)
            return;
        // slow the player when acceleration is not applied
        if (_accInput == 0 && _velocityForward > Settings.MinSpeed)
        {
            _rigidbody2D.drag = Mathf.Lerp(_rigidbody2D.drag, 3f, Time.fixedDeltaTime * 3);
        }
        else
        {
            _rigidbody2D.drag = 0;
        }
        Vector2 forceFactor = transform.up * _accInput * Settings.AccFactor;
        _rigidbody2D.AddForce(forceFactor,ForceMode2D.Force);
    }

    void ApplySteeringForce()
    {
        // limit the ability to turn when moving slowly
        float minVelocityAllowingTurn = (_rigidbody2D.velocity.magnitude / 8);
        minVelocityAllowingTurn = Mathf.Clamp01(minVelocityAllowingTurn);
        _rotationAngle -= _steeringInput * Settings.TurnFactor * minVelocityAllowingTurn;
        _rigidbody2D.MoveRotation(_rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        _steeringInput = inputVector.x;
        _accInput = inputVector.y;
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_rigidbody2D.velocity, transform.up);
        Vector2 lateralVelocity = transform.right * Vector2.Dot(_rigidbody2D.velocity, transform.right);
        // slow the lateral velocity to realize drift effects
        _rigidbody2D.velocity = forwardVelocity + lateralVelocity * Settings.DriftFactor;
    }

    private IEnumerator PauseTrail()
    {
        yield return new WaitForSeconds(0.6f);
        _trailRenderer.enabled = true;
    }

    /// <summary>
    /// In the gravity field of a planet
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Gravity"))
        {
            Vector2 drawDir = (other.GetComponent<Transform>().position - transform.position).normalized;
            _rigidbody2D.AddForce(drawDir*Settings.GravityFactor, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gravity"))
        {
            Vector2 drawDir = (other.GetComponent<Transform>().position - transform.position).normalized;
            _rigidbody2D.AddForce(drawDir*GameObject.Find("GravityField").GetComponent<PlanetManager>().gravity*0.8f, ForceMode2D.Impulse);
            //other.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.5f);
        }
    }
    
    /// <summary>
    /// broom inserted into planet and you cannot leave!
    /// </summary>
    private void SlowDie()
    {
        if (_rigidbody2D.velocity == Vector2.zero)
        {
            GetComponent<SpriteRenderer>().DOFade(0f, 5f);
        }
        else
        {
            DOTween.KillAll();
            GetComponent<SpriteRenderer>().DOFade(1f, 0.6f);
        }
    }
}
