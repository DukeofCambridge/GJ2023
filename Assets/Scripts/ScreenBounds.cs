using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{
    public Camera mainCamera;
    private BoxCollider2D _boxCollider;

    [FormerlySerializedAs("ExitTriggerFired")] 
    public UnityEvent<Collider2D> exitTriggerFired;

    [SerializeField]
    private float teleportOffset = 0.2f;

    [SerializeField]
    private float cornerOffset = 1;

    private void Awake()
    {
        this.mainCamera.transform.localScale = Vector3.one;
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.isTrigger = true;
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        UpdateBoundsSize();
    }

    public void UpdateBoundsSize()
    {
        //orthographicSize = half the size of the height of the screen. That is why we * it by 2
        float ySize = mainCamera.orthographicSize * 2;
        //width of the camera depends on the acpect ration and the height
        Vector2 boxColliderSize = new Vector2(ySize * mainCamera.aspect, ySize);
        _boxCollider.size = boxColliderSize;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        exitTriggerFired?.Invoke(collision);
    }

    public bool AmIOutOfBounds(Vector3 worldPosition)
    {
        return
            Mathf.Abs(worldPosition.x) > Mathf.Abs(_boxCollider.bounds.min.x) ||
            Mathf.Abs(worldPosition.y) > Mathf.Abs(_boxCollider.bounds.min.y);

    }

    public Vector2 CalculateWrappedPosition(Vector2 worldPosition)
    {
        bool xBoundResult = 
            Mathf.Abs(worldPosition.x) > (Mathf.Abs(_boxCollider.bounds.min.x) - cornerOffset);
        bool yBoundResult = 
            Mathf.Abs(worldPosition.y) > (Mathf.Abs(_boxCollider.bounds.min.y) - cornerOffset);

        Vector2 signWorldPosition = 
            new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));

        if (xBoundResult && yBoundResult)
        {
            return Vector2.Scale(worldPosition, Vector2.one * -1) 
                + Vector2.Scale(new Vector2(teleportOffset, teleportOffset), 
                signWorldPosition);
        }
        else if (xBoundResult)
        {
            return new Vector2(worldPosition.x * -1, worldPosition.y) 
                + new Vector2(teleportOffset * signWorldPosition.x, teleportOffset);
        }
        else if (yBoundResult)
        {
            return new Vector2(worldPosition.x, worldPosition.y * -1) 
                + new Vector2(teleportOffset, teleportOffset * signWorldPosition.y);
        }
        else
        {
            return worldPosition;
        }
    }

}