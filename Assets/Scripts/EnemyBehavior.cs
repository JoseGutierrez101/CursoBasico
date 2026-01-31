using System.Collections;
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
    [SerializeField]private float _minTimer;
    [SerializeField]private float _maxTimer;
    private float _chosenTimer;

    [SerializeField] private float _enemyHP;
    private SpriteRenderer _spriteRenderer;
    private Color _color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("bwow " + collision.tag);
        if (collision.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player") && collision.TryGetComponent<ClaseHola>(out ClaseHola player))
        {
            player.TakeDamage(_enemyHP);
            Destroy(gameObject);
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

        StartCoroutine(FlashRed());

        if (_enemyHP <=0 )
        {
            GameObject.Find("Canvas").GetComponent<GameUIBehavior>().EnemyKilled();
            GameObject.Find("Player").GetComponent<ClaseHola>().EnemyKilled();
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashRed ()
    {
        _spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _spriteRenderer.color = _color;
    }
}
