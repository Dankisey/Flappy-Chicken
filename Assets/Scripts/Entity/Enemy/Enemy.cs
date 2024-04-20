using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyEggThrower))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IPoolableObject
{
    [SerializeField] private float _speed;

    private Health _health;
    private Rigidbody2D _rigidBody;
    private Transform _transform;
    private EnemyEggThrower _eggThrower;

    public event Action<IPoolableObject> ReturnPullEvent;

    public void Reset()
    {
        _rigidBody.gravityScale = 0;
        _health.Heal();
        _eggThrower.Reset();
    }

    public void Die()
    {
        _rigidBody.gravityScale = 1;
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _eggThrower = GetComponent<EnemyEggThrower>();
        _rigidBody.gravityScale = 0;
        _transform = transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground ground))
            ReturnPullEvent?.Invoke(this);
        else if(collision.TryGetComponent(out EnemyBorder enemyBorder))
            ReturnPullEvent?.Invoke(this);
        else if (collision.TryGetComponent(out Player player))
            player.TakeHit();
    }

    private void Update()
    {
        _transform.position += Vector3.left * _speed * Time.deltaTime;
    }
}