using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _value = 3;
    [SerializeField] private float _recoveryTime;

    private bool _canTakeDamage = true;

    public event UnityAction Hitted;
    public event UnityAction Died;

    public void Heal()
    {
        _value++;
    }

    public void TakeHit()
    {
        if (_canTakeDamage == false)
            return;

        if (_value < 1)
            return;

        _value--;
        Hitted?.Invoke();

        if (_value < 1)
            Died?.Invoke();
        else
            StartCoroutine(Recover());
    }

    private IEnumerator Recover()
    {
        _canTakeDamage = false;

        yield return new WaitForSeconds(_recoveryTime);

        _canTakeDamage = true;
    }
}