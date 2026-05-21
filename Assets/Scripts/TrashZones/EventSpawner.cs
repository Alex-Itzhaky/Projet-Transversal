using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _trashPrefabs = new GameObject[3];
    [SerializeField] private GameObject _belugaPrefab;
    [SerializeField] private GameObject _hydrophonePrefab;

    [Header("TrashZone Size Settings")]
    [SerializeField] private float _smallTrashZoneChance;
    [SerializeField] private float _mediumTrashZoneChance;
    [SerializeField] private float _largeTrashZoneChance;

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

    private GameObject GetRandomTrashSize()
    {
        float randTrash = Random.Range(0f, 1f);
        if (randTrash < _smallTrashZoneChance)
            return _trashPrefabs[0];
        else if (randTrash < _smallTrashZoneChance + _mediumTrashZoneChance)
            return _trashPrefabs[1];
        else if (randTrash < _smallTrashZoneChance + _mediumTrashZoneChance + _largeTrashZoneChance)
            return _trashPrefabs[2];
        else
            return _trashPrefabs[0];
    }

    public void SpawnTrashZone()
    {
        GameObject trashZoneInstance = Instantiate(GetRandomTrashSize());
        trashZoneInstance.transform.position = GetRandomPosition();
    }

    public void SpawnBeluga()
    {
        GameObject belugaInstance = Instantiate(_belugaPrefab);
        belugaInstance.transform.position = GetRandomPosition();
    }

    public void SpawnHydrophone()
    {
        GameObject hydrophoneInstance = Instantiate(_hydrophonePrefab);
        hydrophoneInstance.transform.position = GetRandomPosition();
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
