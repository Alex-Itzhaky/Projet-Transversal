using UnityEngine;
using System.Collections;

public class TrashBoat : Boat
{

    private TrashZone _currentTrashZone;
    [SerializeField] private float _trashCollectDuration;

    [SerializeField] private TriggerRelay _relay;

    [SerializeField] private AudioClip _cleanTrashSound;

    private void Awake()
    {
        _relay.OnTriggered += OnTrashEnter;
    }

    private void OnDestroy()
    {
        _relay.OnTriggered -= OnTrashEnter;
    }
    private IEnumerator CollectTrashCoroutine()
    {
        _canBoatMove = false;
        _currentBoatState = BoatState.CollectingTrash;
        _targetPosition = _currentTrashZone.transform.position;
        while (_rb.linearVelocity.magnitude > .1f)
        {
            yield return null;
        }
        Debug.Log("Lock trashZone fini, commence le nettoyage");
        yield return new WaitForSeconds(_trashCollectDuration);
        Debug.Log("Nettoyage fini");
        PlayCleanTrashSound();
        _currentBoatState = BoatState.Idle;
        Destroy(_currentTrashZone.gameObject);
        _canBoatMove = true;

    }

    private void PlayCleanTrashSound()
    {
        SoundFXManager.Instance.PlaySoundFXClip( _cleanTrashSound, transform);
    }

    private void OnTrashEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            if (collision.GetComponent<TrashZone>().isBelugaTrappedInside || _currentBoatState == BoatState.CollectingTrash)
                return;
            _currentBoatState = BoatState.CollectingTrash;
            _currentTrashZone = collision.gameObject.GetComponent<TrashZone>();
            StartCoroutine(CollectTrashCoroutine());
        }
    }
}
