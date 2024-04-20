using System;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool<T> where T : MonoBehaviour, IPoolableObject
{
    private Queue<T> _prefabs;
    private T _prefab;
    private int _startAmount;

    public ObjectPool(T prefab, int startAmount)
    {
        _prefab = prefab;
        _startAmount = startAmount;
        _prefabs = new Queue<T>();
    }

    public ObjectPool<T> Initialize()
    {
        for (int i = 0; i < _startAmount; i++)
        {
            T prefab = GetNew();
            Push(prefab);
        }

        return this;
    }

    public T Pull()
    {
        if (_prefabs.Count == 0)
            return GetNew();

        T prefab = _prefabs.Dequeue();
        prefab.gameObject.SetActive(true);

        return prefab;
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        _prefabs.Enqueue(obj);
    }

    public void SubscribeReturnEvent(T obj)
    {
        obj.ReturnPullEvent += OnReturnPoolEvent;
    }

    private void OnReturnPoolEvent(IPoolableObject obj)
    {
        obj.ReturnPullEvent -= OnReturnPoolEvent;
        Push((T)obj);
    }

    private T GetNew()
    {
        return MonoBehaviour.Instantiate(_prefab);
    }
}

public interface IPoolableObject
{
    public event Action<IPoolableObject> ReturnPullEvent;
}