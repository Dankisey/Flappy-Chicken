using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationController : MonoBehaviour
{
    private const string Throw = nameof(Throw);
    private const string Hit = nameof(Hit);

    [SerializeField] private EggThrower _eggThrower;
    [SerializeField] private Health _health;

    private Animator _animator;

    public void Reset()
    {
        _animator.Rebind();
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _eggThrower.Throwed += OnThrowed;
        _health.Hitted += OnHitted;
    }

    private void OnDisable()
    {
        _eggThrower.Throwed -= OnThrowed;
        _health.Hitted -= OnHitted;
    }

    private void OnThrowed()
    {
        _animator.SetTrigger(Throw);
    }

    private void OnHitted()
    {
        _animator.SetTrigger(Hit);
    }
}