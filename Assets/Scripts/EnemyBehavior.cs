using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float _enemyYSpeed;
    [SerializeField] private float _enemyXSpeed;
    private float _movingX;
    private float _enemyWidth;
    private Vector2 _screenBounds;
    private Collider2D _collider;

    [SerializeField] private GameObject _bullet;
    private float _minTimer = 0.5f;
    private float _maxTimer = 1.5f;
    private float _chosenTimer;

    [SerializeField] private float _enemyHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _chosenTimer = Random.Range(_minTimer, _maxTimer);

        Vector3 screenValues = new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z);
        _screenBounds = Camera.main.ScreenToWorldPoint(screenValues);

        _collider = GetComponent<Collider2D>();
        _enemyWidth = _collider.bounds.extents.x;

        ChangeDestination();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemyYSpeed);

        float speed = _enemyXSpeed * Time.deltaTime;
        float chosenX = Mathf.MoveTowards(transform.position.x, _movingX, speed);
        transform.position = new Vector3(chosenX, transform.position.y, transform.position.z);

        if (Mathf.Abs(chosenX - _movingX) < 0.1f)
        {
            ChangeDestination();
        }


        Shoot();
    }

    private void Shoot ()
    {
        _chosenTimer -= Time.deltaTime;

        if (_chosenTimer <= 0)
        {
            Instantiate(_bullet, transform.position, Quaternion.identity);
            _chosenTimer = Random.Range(_minTimer, _maxTimer);
        }
    }

    private void ChangeDestination()
    {
        _movingX = Random.Range(-_screenBounds.x + _enemyWidth, _screenBounds.x - _enemyWidth);
    }

    public void TakeDamage (float damage)
    {
        //Debug.Log("Ouch!" + damage);
        _enemyHP -= damage;
        if (_enemyHP <=0 )
        {
            Destroy(gameObject);
        }
    }
}
