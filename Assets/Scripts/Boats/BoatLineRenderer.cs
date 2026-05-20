using System;
using UnityEngine;

public class BoatLineRenderer : MonoBehaviour
{
    [SerializeField] private Boat _boat;
    [SerializeField] private LineRenderer _lineRenderer;

    public void Update()
    {
        DrawLine();
    }

    public void DrawLine()
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _boat._targetPosition);
        _lineRenderer.startWidth = 0.25f;
        _lineRenderer.endWidth = 0.25f;
    }
}
