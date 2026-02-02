using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class ClaseHola : MonoBehaviour
{
    //BasicMove
    [SerializeField] private float _smoothTime;
    private Vector2 _fixedInput;
    private Vector2 _input;
    private Vector2 _screenBounds;
    private float _playerWidth;
    private float _playerHeight;

    //Speeding
    [SerializeField] private int _playerSpeed;
    private float _speedMult = 1f;
    private Vector2 _currentVelocity = Vector2.zero;

    //Shooting
    [SerializeField] private float _cooldown;
    [SerializeField] private GameObject _bullet;
    private float _shootTimer = 0;
    private bool _shooting;

    //Damage
    [SerializeField] private GameObject _deathFX;
    [SerializeField] private float _health;
    private float _maxHealth;
    //private RectTransform _hpRectTransform;
    private Color _color;
    
    //Components
    private PlayerInput _playerInput;
    private Rigidbody2D _rigbody;
    private Animator _animator;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start");
        _playerInput = GetComponent<PlayerInput>();
        _rigbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();


        //_hpRectTransform = GameObject.Find("HealthLabel").GetComponent<RectTransform>();
        _maxHealth = _health;
        _color = _spriteRenderer.color;

        Vector3 screenValues = new Vector3 (Screen.width, Screen.height, Camera.main.transform.position.z);
        _screenBounds = Camera.main.ScreenToWorldPoint(screenValues);
        _playerHeight = _collider.bounds.extents.y;
        _playerWidth = _collider.bounds.extents.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ReadInput();
        Shoot();

        _fixedInput = Vector2.SmoothDamp(_fixedInput, _input, ref _currentVelocity, _smoothTime);
        _rigbody.MovePosition(_rigbody.position + _fixedInput * _playerSpeed*_speedMult * Time.fixedDeltaTime);
        _animator.SetFloat("InputX", _input.x);

        //Debug.Log("Screenwidth: " + Screen.width + "   ScreenHeight: " + Screen.height);
    }

    private void LateUpdate ()
    {
        Vector3 curPos = transform.position;
        curPos.x = Mathf.Clamp(curPos.x, -_screenBounds.x + _playerWidth, _screenBounds.x - _playerWidth);
        curPos.y = Mathf.Clamp(curPos.y, -_screenBounds.y + _playerHeight, _screenBounds.y - _playerHeight);
        transform.position = curPos;
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

    public void TakeDamage(float damage)
    {
        _health -= damage;

        StartCoroutine(FlashRed(true));

        GameObject.Find("Canvas").GetComponent<GameUIBehavior>().PlayerHurt(_health, _maxHealth);

        if (_health <= 0)
        {
            GameObject.Find("Canvas").GetComponent<GameUIBehavior>().PlayerKilled();
            Instantiate(_deathFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashRed (bool actuallyRed)
    {
        if (actuallyRed) _spriteRenderer.color = Color.red;
        else _spriteRenderer.color = Color.green;
        

        yield return new WaitForSeconds(0.1f);

        _spriteRenderer.color = _color;
    }

    public void EnemyKilled()
    {
        if (!(_health==_maxHealth)) StartCoroutine(FlashRed(false));
        _health += 10;
        if (_health > _maxHealth) _health = _maxHealth;
        GameObject.Find("Canvas").GetComponent<GameUIBehavior>().PlayerHurt(_health, _maxHealth);
    }
}
