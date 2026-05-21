using UnityEngine;
using System.Collections;

public class SaveBoat : Boat
{
    private TrashZone _currentTrashZone;
    [SerializeField] private float _timeToHeal;
    public bool isSavingBeluga;
    private Beluga _currentBeluga;
    public Inventory inventory;

    [SerializeField] private TriggerRelay _relay;

    [SerializeField] private AudioClip _saveBelugaSFX;

    private void Awake()
    {
        _relay.OnTriggered += OnHealEnter;
    }

    private void OnDestroy()
    {
        _relay.OnTriggered -= OnHealEnter;
    }

    protected override void OnCollisionEnter2D(Collision2D collision){ }

    private void OnHealEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            TrashZone trashZone = collision.gameObject.GetComponent<TrashZone>();
            Debug.Log(trashZone.isBelugaTrappedInside);
            if (trashZone.isBelugaTrappedInside)
            {
                _currentTrashZone = trashZone;
                _currentBoatState = BoatState.Repairing;
                _currentBeluga = trashZone.currentBelugaTrapped;
                StartCoroutine(HealBelugaCoroutine());
            }
        }
    }

    private IEnumerator HealBelugaCoroutine()
    {
        _canBoatMove = false;
        _currentBoatState = BoatState.Repairing;
        _targetPosition = _currentTrashZone.transform.position;
        while (_rb.linearVelocity.magnitude > .1f)
        {
            yield return null;
        }
        Debug.Log("Lock Heal fini, commence le soin");
        yield return new WaitForSeconds(_timeToHeal);
        _currentBeluga.HealBeluga();
        _currentBoatState = BoatState.Idle;
        _currentTrashZone.currentBelugaTrapped = null;
        _currentTrashZone.isBelugaTrappedInside = false;
        SoundFXManager.Instance.PlaySoundFXClip(_saveBelugaSFX, transform);
        _canBoatMove = true;
        inventory.AddPoints(100);
    }
}

