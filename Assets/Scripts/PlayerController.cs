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
        ApplyAccForce();
        ApplySteeringForce();
    }

    void ApplyAccForce()
    {
        Vector2 forceFactor = transform.up * _accInput * Settings.Acc;
        _rigidbody2D.AddForce(forceFactor,ForceMode2D.Force);
    }

    void ApplySteeringForce()
    {
        _rotationAngle -= _steeringInput * Settings.Turn;
        _rigidbody2D.MoveRotation(_rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        _steeringInput = inputVector.x;
        _accInput = inputVector.y;
    }
    
}
