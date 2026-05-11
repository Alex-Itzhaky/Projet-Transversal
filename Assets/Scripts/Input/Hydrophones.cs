using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hydrophones : MonoBehaviour
{
    private float _range;
    private bool _belugaDetected;
    private bool _isBroken;
    private bool _isDirty;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        TriggerEvent();
    }

    private void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (_belugaDetected)
        {
            _spriteRenderer.color = new Color(255, 0, 0);
        }
        else if (_isBroken)
        {
            _spriteRenderer.color = new Color(0, 0, 0);
        }
        else if (_isDirty)
        {
            _spriteRenderer.color = new Color(0, 255, 0);
        }
        else
        {
            _spriteRenderer.color = new Color(0, 0, 255);
        }
    }

    private IEnumerator BelugaCoroutine()
    {
        Debug.Log("Avant le timer");
        yield return new WaitForSeconds(Random.Range(5,10));
        if (Random.Range(1, 3) == 1)
        {
            _isDirty = true;
        }
        else if (Random.Range(1, 3) == 2)
        {
            _isBroken = true;
        }
        else
        {
            _belugaDetected = true;
        }
        Debug.Log("Timer fini");
    }
    
    void TriggerEvent()
    {
        if (!_belugaDetected)
        {
            StartCoroutine(BelugaCoroutine());
        }
    }
}
