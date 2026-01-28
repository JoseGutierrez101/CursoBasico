using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _damage;

    private CircleCollider2D _collider;

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Ive been touched by " + other.tag);
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3 (0,_bulletSpeed * Time.deltaTime,0);
    }
}
