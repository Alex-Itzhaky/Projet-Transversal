using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Gameloop : MonoBehaviour
{
    [SerializeField] private float _baseBelugaSpawnTimer;
    [SerializeField] private float _baseTrashSpawnTimer;

    [SerializeField] private EventSpawner _eventSpawner;

    private float _currentBelugaSpawnTimer;
    private float _currentTrashSpawnTimer;

    private float _elapsedTime;

    [SerializeField] private List<float> _timesToDecreaseBelugaTimer = new List<float>();
    [SerializeField] private List<float> _timesToDecreaseTrashTimer = new List<float>();

    private bool _canBelugaSpawn = true;
    private bool _canTrashSpawn = true;

    private IEnumerator Start()
    {
        _eventSpawner.SpawnTrashZone();
        _currentBelugaSpawnTimer = _baseBelugaSpawnTimer;
        _currentTrashSpawnTimer = _baseTrashSpawnTimer;
        yield return new WaitForSeconds(5);
        _eventSpawner.SpawnBeluga();
    }

    private void Update()
    {
        if (_canBelugaSpawn)
            StartCoroutine(BelugaSpawnTimer(GetRandomizedTimer(_baseBelugaSpawnTimer)));
        if (_canTrashSpawn)
            StartCoroutine(TrashSpawnTimer(GetRandomizedTimer(_baseTrashSpawnTimer)));
        HandleTimersDecrease();
        _elapsedTime += Time.deltaTime;
    }

    private IEnumerator BelugaSpawnTimer(float time)
    {
        _canBelugaSpawn = false;
        yield return new WaitForSeconds(time);
        _canBelugaSpawn = true;
        _eventSpawner.SpawnBeluga();
    }
    
    private IEnumerator TrashSpawnTimer(float time)
    {
        _canTrashSpawn = false;
        yield return new WaitForSeconds(time);
        _canTrashSpawn = true;
        _eventSpawner.SpawnTrashZone();
    }

    private float GetRandomizedTimer(float time)
    {
        float timer = Random.Range(time / 1.5f, time * 1.5f);
        return timer;
    }

    private void HandleTimersDecrease()
    {
        foreach (float timeToReach in _timesToDecreaseTrashTimer)
        {
            if (_elapsedTime > timeToReach)
            {
                _currentTrashSpawnTimer = DecreaseTimer(_currentTrashSpawnTimer, 1.5f);
                return;
            }
        }

        foreach (float timeToReach in _timesToDecreaseBelugaTimer)
        {
            if (_elapsedTime > timeToReach)
            {
                _currentBelugaSpawnTimer = DecreaseTimer(_currentBelugaSpawnTimer, 1.5f);
                return;
            }
        }
    }

    private float DecreaseTimer(float time,float factor)
    {
        return time / factor;
    }
}
