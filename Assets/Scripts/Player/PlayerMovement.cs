using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 10f;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movePlayer();
    }


    void movePlayer()
    {
        movement = InputManager.movement;
        rb.linearVelocity = movement * moveSpeed;
    }
}
