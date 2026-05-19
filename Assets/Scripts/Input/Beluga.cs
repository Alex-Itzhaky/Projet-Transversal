using System;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Beluga : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _belugaSprite;
    private float _moveSpeed = 2.5f;
    private float _waitingTimer = 0f;
    private bool _isBelugaMoving = false;
    private bool _isBelugaSick = false;
    private bool _isBelugaDead = false;
    [SerializeField] private float _timeToDie;
    
    private void BelugaMove()
    {
        if (_isBelugaDead || _isBelugaSick)
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
        else
        {
            Vector3 direction = Vector3.zero;
            direction.x += Random.Range(-2, 2);
            direction.y += Random.Range(-2, 2);
            direction.Normalize();
            _rigidbody2D.linearVelocity = direction * _moveSpeed;
        }
    }
    
    private IEnumerator MoveCoroutine()
    {
        
        yield return new WaitForSeconds(3);
        BelugaMove();
    }
    
    private void Update()
    {
        _waitingTimer += Time.deltaTime;
        if (_waitingTimer >= Random.Range(1, 2))
        {
            _waitingTimer = 0f;
            if (!_isBelugaMoving)
            {
                BelugaMove();
                _isBelugaMoving = true;
            }
            else
            {
                Vector3 stop = new Vector3(0, 0, 0);
                _rigidbody2D.linearVelocity = stop;
                _isBelugaMoving = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boat") || other.gameObject.CompareTag("Hydrophone"))
        {
            Debug.Log("RevealBeluga");
            _belugaSprite.DOFade(1, 1);
        }
        if (other.gameObject.CompareTag("Trash"))
        {
            Debug.Log(other.gameObject.tag);
            TrashZone trashZone = other.gameObject.GetComponent<TrashZone>();
            Debug.Log(trashZone.name);
            if (!trashZone.isBelugaTrappedInside)
            {
                trashZone.currentBelugaTrapped = this;
                trashZone.isBelugaTrappedInside = true;
                StartCoroutine(BelugaSicknessCoroutine());
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boat") || other.gameObject.CompareTag("Hydrophone"))
        {
            Debug.Log("HideBeluga");
            _belugaSprite.DOFade(0, 1);
        }
    }

    public void HealBeluga()
    {
        //Jouer les anims/particules si y en a
        //Rajouter les points de reputation
        Debug.Log("Heal Beluga");
        _belugaSprite.DOFade(0, 1).OnComplete(()=> Destroy(gameObject));
    }

    private IEnumerator BelugaSicknessCoroutine()
    {
        Debug.Log("Commence � die le beluga");
        _isBelugaSick = true;
        yield return new WaitForSeconds(_timeToDie);
        _isBelugaDead = true;
        _isBelugaSick = false;
        KillBeluga();
    }

    private void KillBeluga()
    {
        //Jouer anims
        //d�duire score
        _belugaSprite.DOFade(0, 1).OnComplete( ()=> Destroy(gameObject));
    }


}
