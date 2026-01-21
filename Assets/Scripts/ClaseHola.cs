using UnityEngine;

public class ClaseHola : MonoBehaviour
{
    [Header("Jugador")]
    [SerializeField] private int velocidad;

    private Vector3 movim = new Vector3(1, 0, 0);
    private Rigidbody2D rigbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start");
        rigbody = GetComponent<Rigidbody2D>();
        //transform.Translate(new Vector3(10, 1));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += movim * Time.deltaTime;
        rigbody.linearVelocity = movim*10;
    }
}
