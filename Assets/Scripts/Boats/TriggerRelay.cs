using UnityEngine;
using System;

public class TriggerRelay : MonoBehaviour
{
    public System.Action<Collider2D> OnTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggered?.Invoke(collision);
    }
}
