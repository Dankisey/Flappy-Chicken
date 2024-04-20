using UnityEngine;
using UnityEngine.Events;

public class Ground : MonoBehaviour
{
    public event UnityAction PlayerFell;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
            PlayerFell?.Invoke();
    }
}