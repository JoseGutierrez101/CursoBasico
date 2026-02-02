using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private GameObject _enemyEasy;
    [SerializeField] private GameObject _enemyMedium;
    [SerializeField] private GameObject _enemyMedium2;
    [SerializeField] private GameObject _enemyHard;
    private float _chosenX;
    private float _timer;
    private Collider2D _collider;
    private GameObject _enemy;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _timer = _spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            _timer = _spawnRate;
            SpawnEnemy(Mathf.FloorToInt(Random.Range(1f, 6.15f)));
            if (!(_spawnRate < 0.5)) _spawnRate *= 0.99f;
            Debug.Log("SpawnRate: " + _spawnRate);
        }
    }

    void SpawnEnemy (int chosenEnemy)
    {
        switch (chosenEnemy)
        {
            default:
            _enemy = _enemyEasy;
            break;
            case 2:
            case 4:
            _enemy = _enemyMedium;
            break;
            case 3:
            _enemy = _enemyMedium2;
            break;
            case 6:
            _enemy = _enemyHard;
            break;
        }

        Instantiate(_enemy, GetRandomPos(), Quaternion.identity);
    }
    Vector3 GetRandomPos ()
    {
        Bounds bounds = _collider.bounds;
        float chosenX = Random.Range(bounds.min.x, bounds.max.x);//pendiente
        return new Vector3(chosenX , bounds.min.y, transform.position.z);
    }
}
