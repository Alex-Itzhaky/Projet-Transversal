using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _trashPrefab;
    [SerializeField] private GameObject _belugaPrefab;

    [Header("TrashZone Radius Settings")]
    [SerializeField] private float _minTrashRadius;
    [SerializeField] private float _maxTrashRadius;

    [Header("TrashSpawner Area Size")]
    [SerializeField] private float _areaWidth;
    [SerializeField] private float _areaHeight;

    private Vector2 GetRandomPosition()
    {
        float randomOffsetX = Random.Range(-_areaWidth / 2, _areaWidth / 2);
        float randomOffsetY = Random.Range(-_areaHeight / 2, _areaHeight / 2);
        Vector2 targetPosition = new Vector2(
            transform.position.x + randomOffsetX,
            transform.position.y + randomOffsetY
        );
        return targetPosition;
    }

    private float GetRandomRadius()
    {
        return Random.Range(_minTrashRadius, _maxTrashRadius);
    }

    public void SpawnTrashZone()
    {
        GameObject trashZoneInstance = Instantiate(_trashPrefab);
        trashZoneInstance.transform.position = GetRandomPosition();
        trashZoneInstance.GetComponent<CircleCollider2D>().radius = GetRandomRadius();
    }

    public void SpawnBeluga()
    {
        GameObject belugaInstance = Instantiate(_belugaPrefab);
        belugaInstance.transform.position = GetRandomPosition();
    }

    //Debugging

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y, .1f), new Vector3(_areaWidth, _areaHeight, .1f));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y, .1f), new Vector3(_areaWidth, _areaHeight, .1f));
    }

    [ContextMenu("Test SpawnTrash")]
    private void TestTrashSpawn()
    {
        SpawnTrashZone();
    }

    [ContextMenu("Test SpawnBeluga")]
    private void TestBelugaSpawn()
    {
        SpawnBeluga();
    }

}
