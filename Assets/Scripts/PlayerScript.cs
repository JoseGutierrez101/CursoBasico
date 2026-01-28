using UnityEngine;
using UnityEngine.InputSystem;

public class ClaseHola : MonoBehaviour
{
    //BasicMove
    [SerializeField] private float _smoothTime;
    private Vector2 _fixedInput;
    private Vector2 _input;

    //Speeding
    [SerializeField] private int _playerSpeed;
    private float _speedMult = 1f;
    private Vector2 _currentVelocity = Vector2.zero;

    //Shooting
    [SerializeField] private float _cooldown;
    [SerializeField] private GameObject _bullet;
    private float _shootTimer = 0;
    private bool _shooting;
    
    //Components
    private PlayerInput _playerInput;
    private Rigidbody2D _rigbody;
    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start");
        _playerInput = GetComponent<PlayerInput>();
        _rigbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        //transform.Translate(new Vector3(10, 1));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ReadInput();
        Shoot();

        _fixedInput = Vector2.SmoothDamp(_fixedInput, _input, ref _currentVelocity, _smoothTime);
        _rigbody.MovePosition(_rigbody.position + _fixedInput * _playerSpeed*_speedMult * Time.fixedDeltaTime);
        _animator.SetFloat("InputX", _input.x);
    }

    private void ReadInput() {
        _input = _playerInput.actions["Move"].ReadValue<Vector2>();
        //Debug.Log("test: " + _playerInput.actions["Speedup"].IsPressed());
        if (_playerInput.actions["Speedup"].IsPressed()) {
            _speedMult = 1.5f;
        } else {
            _speedMult = 1f;
        }
        _shooting = _playerInput.actions["Shoot"].IsPressed();
    }
    private void Shoot()
    {
        GameObject curBullet;
        _shootTimer -= Time.fixedDeltaTime;
        if (_shooting && (_shootTimer <= 0))
        {
            _shootTimer = _cooldown;
            curBullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        }
    }
}
