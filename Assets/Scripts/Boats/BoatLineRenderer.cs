using UnityEngine;

public class BoatLineRenderer : MonoBehaviour
{
    [SerializeField] private Boat _boat;
    [SerializeField] private LineRenderer _lineRenderer;
    
    public void DrawLine()
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _boat._targetPosition);
    }
}
