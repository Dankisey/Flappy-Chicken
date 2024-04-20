using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _startAmount = 3;
    [SerializeField] private int _cooldown;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _minHeight;
    [SerializeField] private Transform _maxHeight;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(_prefab, _startAmount).Initialize();

        StartCoroutine(SpawningCycle());
    }

    private IEnumerator SpawningCycle()
    {
        var wait = new WaitForSeconds(_cooldown);

        while (true)
        {
            float heightFactor = Random.Range(0f, 1f);
            Vector3 spawnPosition = Vector3.Lerp(_minHeight.position, _maxHeight.position, heightFactor);

            Enemy enemy = _pool.Pull();
            enemy.Reset();
            _pool.SubscribeReturnEvent(enemy);
            enemy.gameObject.transform.position = spawnPosition;

            yield return wait;
        }
    }
}