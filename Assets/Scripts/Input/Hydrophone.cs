using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Hydrophone : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _belugaSprite;
    private bool _isBroken = false;
    private Color _neutral = new Color(255,255,255);
    private Color _belugaDetected = new Color(255,0,0);
    private Color _broken = new Color(0, 0, 0);

    [SerializeField] private AudioClip _detectionSFX;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Beluga"))
        {
            if (!_isBroken)
            {
                _spriteRenderer.color = _belugaDetected;
                SoundFXManager.Instance.PlaySoundFXClip(_detectionSFX, transform);
            }
            else
            {
                _spriteRenderer.color = _broken;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Beluga"))
        {
            if (!_isBroken)
            {
                _spriteRenderer.color = _neutral;
            }
            else
            {
                _spriteRenderer.color = _broken;
            }
        }
    }

    private IEnumerator BelugaCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(5,10));
        _isBroken = true;
        Debug.Log("broke");
    }
}
