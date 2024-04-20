using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Flapper : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _force = 7f;
    [SerializeField] private float _rotationSpeed = 1.2f;
    [SerializeField] private float _minAngle = -45f;
    [SerializeField] private float _maxAngle = 45f;

    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Quaternion _minRotation;
    private Quaternion _maxRotation;
    private bool _canFlap = true;

    public event UnityAction Flapped;

    public void SetFlapPossibility(bool value)
    {
        _canFlap = value;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _minRotation = Quaternion.Euler(0, 0, _minAngle);
        _maxRotation = Quaternion.Euler(0, 0, _maxAngle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))      
            TryFlap();

        LookDown();
    }

    private void TryFlap()
    {
        if (_canFlap == false)
            return;

        _rigidbody.velocity = Vector2.up * _force;
        _transform.rotation = _maxRotation;
        Flapped?.Invoke();
    }

    private void LookDown()
    {
        _transform.rotation =  Quaternion.Lerp(_transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    private void OnDied()
    {
        SetFlapPossibility(false);
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }
}