using UnityEngine;
using Random = UnityEngine.Random;

public class Beluga : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody2D;
    private float _moveSpeed = 2.5f;
    private float _waitingTimer = 0f;
    
    private void BelugaMove()
    {
        Vector3 direction = Vector3.zero;
        direction.x += Random.Range(-2, 2);
        direction.y += Random.Range(-2, 2);
        direction.Normalize();
        _rigidbody2D.linearVelocity = direction * _moveSpeed;

    }
    
    private void Update()
    {
        _waitingTimer += Time.deltaTime;
        if (_waitingTimer >= Random.Range(1, 2))
        {
            _waitingTimer = 0f;
            BelugaMove();
        }
    }
    
}
