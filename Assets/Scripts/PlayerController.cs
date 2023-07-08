using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float _accInput;
    private float _steeringInput;
    private float _rotationAngle;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // acceleration
        ApplyAccForce();
        // kill the lateral velocity to realize drift effects
        KillOrthogonalVelocity();
        // steer the rotation
        ApplySteeringForce();
    }

    void ApplyAccForce()
    {
        Vector2 forceFactor = transform.up * _accInput * Settings.AccFactor;
        _rigidbody2D.AddForce(forceFactor,ForceMode2D.Force);
    }

    void ApplySteeringForce()
    {
        _rotationAngle -= _steeringInput * Settings.TurnFactor;
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

        _rigidbody2D.velocity = forwardVelocity + lateralVelocity * Settings.DriftFactor;
    }
}
