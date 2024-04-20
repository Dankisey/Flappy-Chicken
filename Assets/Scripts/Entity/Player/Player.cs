using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour 
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void TakeHit()
    {
        _health.TakeHit();
    }
}