using System;
using UnityEngine;

public abstract class Egg : MonoBehaviour, IPoolableObject
{
    [SerializeField] private float _speed;

    private Transform _transform;

    public event Action<IPoolableObject> ReturnPullEvent;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        Vector3 velocity = -_transform.up * _speed * Time.deltaTime;
        _transform.position += velocity;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EggBorder border))      
            ReturnPullEvent?.Invoke(this);       
        else     
            OnEntityCollided(collision);               
    }

    protected abstract void OnEntityCollided(Collider2D collision);
}