using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float _enemyYSpeed;
    [SerializeField] private float _enemyXSpeed;
    [SerializeField] private GameObject _bullet;

    private float _minTimer = 0.5f;
    private float _maxTimer = 1.5f;
    private float _chosenTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _chosenTimer = Random.Range(_minTimer, _maxTimer);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemyYSpeed);
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
}
