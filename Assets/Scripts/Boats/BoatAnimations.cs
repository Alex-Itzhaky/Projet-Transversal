using UnityEngine;

public class BoatAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _sprite;

    private void Update()
    {
        HandleAnimations();
        Flip();
    }

    private void HandleAnimations()
    {
        if (Mathf.Abs(_rb.linearVelocity.x) > Mathf.Abs(_rb.linearVelocity.y))
        {
            _animator.SetBool("isMovingOnX", true);
            _animator.SetFloat("yVelocity", 0f);
        }
        else if (Mathf.Abs(_rb.linearVelocity.x) < Mathf.Abs(_rb.linearVelocity.y))
        {
            _animator.SetBool("isMovingOnX", false);
            _animator.SetFloat("yVelocity", _rb.linearVelocity.y);
        }
    }

    private void Flip()
    {
        if (_rb.linearVelocity.x < -.1f)
        {
            _sprite.flipX = false;
        }
        else if (_rb.linearVelocity.x > +.1f)
        {
            _sprite.flipX = true;
        }
    }
}
