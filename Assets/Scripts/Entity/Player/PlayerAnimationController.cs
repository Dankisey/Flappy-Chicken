using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private const string IsAlive = nameof(IsAlive);
    private const string Throw = nameof(Throw);
    private const string Flap = nameof(Flap);
    private const string Hit = nameof(Hit);

    [SerializeField] private EggThrower _eggThrower;
    [SerializeField] private Flapper _flapper;
    [SerializeField] private Health _health;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(IsAlive, true);
    }

    private void OnEnable()
    {
        _eggThrower.Throwed += OnThrowed;
        _flapper.Flapped += OnFlapped;
        _health.Hitted += OnHitted;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _eggThrower.Throwed -= OnThrowed;
        _flapper.Flapped -= OnFlapped;
        _health.Hitted -= OnHitted;
        _health.Died -= OnDied;
    }

    private void OnThrowed()
    {
        _animator.SetTrigger(Throw);
    }

    private void OnHitted()
    {
        _animator.SetTrigger(Hit);
    }

    private void OnDied()
    {
        _animator.SetBool(IsAlive, false);
    }

    private void OnFlapped()
    {
        _animator.SetTrigger(Flap);
    }
}