using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Boat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CapsuleCollider2D _collider;
    [SerializeField] public Grid grid;
    [SerializeField] public Tilemap islandTilemap;
    private GameObject _currentTrashZone;

    [Header("Variables")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _trashCollectDuration;
    private BoatState _currentBoatState;
    private Vector2 _targetPosition;
    private bool _isBoatSelected = false;
    private bool _canBoatMove = true;

    public UnityEvent BoatIsSelected;
    public UnityEvent BoatIsUnselected;

    private void Start()
    {
        _currentBoatState = BoatState.Idle;
    }

    private void Update()
    {
        SetNewTargetPositionOnClick();//Important de garder cet ordre de priorité sinon on désélectionne le bateau avant de choisir la nouvelle destination
        SelectBoat();
        CheckGameOver();
    }

    private void FixedUpdate()
    {
        FollowTargetPosition();
        //RotateTowardsTargetPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            _currentBoatState = BoatState.CollectingTrash;
            _currentTrashZone = collision.gameObject;
            StartCoroutine(CollectTrashCoroutine());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Beluga"))
        {
            //Trigger game over
            Debug.Log("Game Over");
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        }
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
        _currentBoatState = BoatState.Idle;
        Destroy(_currentTrashZone);
        _canBoatMove = true;

    }

    private void SelectBoat()
    {
        if (!InputManager.Instance.IsLeftClicking || _currentBoatState == BoatState.CollectingTrash || _currentBoatState == BoatState.Repairing)
            return;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider == _collider)
        {
            _isBoatSelected = true;
            BoatIsSelected.Invoke();
            Debug.Log($"Bateau Sélectionné : {_isBoatSelected}");
        }
        else
        {
            _isBoatSelected = false;
            BoatIsUnselected.Invoke();
            Debug.Log($"Bateau Désélectionné : {_isBoatSelected}");
        }
    }

    private void SetNewTargetPositionOnClick()
    {
        if (!InputManager.Instance.IsLeftClicking || !_isBoatSelected || _currentBoatState == BoatState.CollectingTrash || _currentBoatState == BoatState.Repairing)
            return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if (islandTilemap.HasTile(gridPosition))
        {
            return;
        }

        _targetPosition = mousePosition;
        Debug.Log($"Target Position : {_targetPosition}");
    }

    private void FollowTargetPosition()
    {
        if (Vector2.Distance((Vector2) transform.position, _targetPosition) < .1f)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }
            
        Vector2 dir = _targetPosition - (Vector2) transform.position;
        dir.Normalize();
        _rb.linearVelocity = dir * _moveSpeed;
    }
    private void CheckGameOver()
    {

    }


    //private void RotateTowardsTargetPosition()
    //{
    //    float angle = Mathf.Atan2(_targetPosition.y - transform.position.y, _targetPosition.x - transform.position.x) * Mathf.Rad2Deg - 90;
    //    transform.eulerAngles = new Vector3(0, 0, angle);
    //}
}