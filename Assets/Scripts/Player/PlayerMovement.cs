using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    [SerializeField] private float _moveSpeed = 10f;
    private Vector2 _movement;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movePlayer();
    }


    void movePlayer()
    {
        _movement = InputManager.movement;
        _rb.linearVelocity = _movement * _moveSpeed;
    }
}
