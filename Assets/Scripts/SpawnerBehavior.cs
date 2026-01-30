using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private GameObject _enemy;
    private float _chosenX;
    private float _timer;
    private Collider2D _collider;


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
            SpawnEnemy();
        }
    }

    void SpawnEnemy ()
    {
        Instantiate(_enemy, GetRandomPos(), Quaternion.identity);
    }
    Vector3 GetRandomPos ()
    {
        Bounds bounds = _collider.bounds;
        float chosenX = Random.Range(bounds.min.x, bounds.max.x);//pendiente
        return new Vector3(chosenX , bounds.min.y, transform.position.z);
    }
}
