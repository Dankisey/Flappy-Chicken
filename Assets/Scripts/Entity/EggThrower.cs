using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class EggThrower : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Transform _eggSpawnpoint;
    [SerializeField] private float _cooldown = 1f;
    [SerializeField] private float _delay = 0.8f;
    [SerializeField] private Egg _prefab;

    private Coroutine _cooldownCoroutine = null;
    private Coroutine _throwCoroutine = null;
    private Quaternion _prefabRotation;
    private ObjectPool<Egg> _pool;
    private bool _canThrow = true;
    private int _poolStartCount = 3;

    public event UnityAction Throwed;

    public void Reset()
    {
        _cooldownCoroutine = null;
        _throwCoroutine = null;
        StopAllCoroutines();
        _canThrow = true;
    }

    protected void TryThrow()
    {
        if (_canThrow == false)
            return;

        if (_cooldownCoroutine != null)
            StopCoroutine(_cooldownCoroutine);

        _cooldownCoroutine = StartCoroutine(DoCooldown());
        _throwCoroutine = StartCoroutine(Throw());
    }

    private void Awake()
    {
        _prefabRotation = _prefab.transform.rotation;
        _pool = new ObjectPool<Egg>(_prefab, _poolStartCount).Initialize();
    }

    private IEnumerator DoCooldown()
    {
        _canThrow = false;

        yield return new WaitForSeconds(_cooldown);

        _canThrow = true;
        _cooldownCoroutine = null;
    }

    private IEnumerator Throw()
    {
        Throwed?.Invoke();

        yield return new WaitForSeconds(_delay);

        Egg egg = _pool.Pull();
        _pool.SubscribeReturnEvent(egg);
        Configure(egg);

        _throwCoroutine = null;
    }

    private void Configure(Egg egg)
    {
        egg.transform.position = _eggSpawnpoint.position;
        egg.transform.rotation = _prefabRotation * transform.rotation;
        egg.gameObject.SetActive(true);
    }

    private void OnHitted()
    {
        if (_cooldownCoroutine != null)
            StopCoroutine(_cooldownCoroutine);

        _cooldownCoroutine = StartCoroutine(DoCooldown());

        if (_throwCoroutine!= null)
            StopCoroutine(_throwCoroutine);
    }

    private void OnEnable()
    {
        _health.Hitted += OnHitted;
    }

    private void OnDisable()
    {
        _health.Hitted -= OnHitted;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}