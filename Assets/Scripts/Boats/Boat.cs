using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Boat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected CircleCollider2D _collider;
    [SerializeField] public Grid grid;
    [SerializeField] public Tilemap islandTilemap;
    [SerializeField] public GameObject _gameOverScreen;
    

    [Header("Variables")]
    [SerializeField] private float _moveSpeed;
    
    protected BoatState _currentBoatState;
    public Vector2 _targetPosition;
    protected bool _isBoatSelected = false;
    protected bool _canBoatMove = true;

    public UnityEvent BoatIsSelected;
    public UnityEvent BoatIsUnselected;

    private void Start()
    {
        _currentBoatState = BoatState.Idle;
        _targetPosition = transform.position;
    }

    private void Update()
    {
        SetNewTargetPositionOnClick();//Important de garder cet ordre de priorit� sinon on d�s�lectionne le bateau avant de choisir la nouvelle destination
        SelectBoat();
        CheckGameOver();
        //Debug.Log($"{gameObject.name} State = {_currentBoatState}");


    }

    private void FixedUpdate()
    {
        FollowTargetPosition();
        //RotateTowardsTargetPosition();
    }

    

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Beluga") && _currentBoatState != BoatState.Repairing)
        {
            //Trigger game over
            Debug.Log("Game Over");
            Time.timeScale = 0f;
            _gameOverScreen.SetActive(true);
            
#if UNITY_EDITOR
            //EditorApplication.ExitPlaymode();
#endif
        }
    }

    

    private void SelectBoat()
    {
        if (!InputManager.Instance.IsLeftClicking || _currentBoatState == BoatState.CollectingTrash || _currentBoatState == BoatState.Repairing)
            return;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        Debug.Log(hit.collider);
        if (hit.collider != null && hit.collider == _collider)
        {
            _isBoatSelected = true;
            BoatIsSelected.Invoke();
            Debug.Log($"Bateau S�lectionn� : {_isBoatSelected}");
        }
        else
        {
            _isBoatSelected = false;
            BoatIsUnselected.Invoke();
            Debug.Log($"Bateau D�s�lectionn� : {_isBoatSelected}");
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