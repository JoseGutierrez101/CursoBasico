using UnityEngine;
using UnityEngine.InputSystem;

public class ClaseHola : MonoBehaviour
{
    [SerializeField] private int _playerSpeed;
    private Vector2 _input;
    private PlayerInput _playerInput;

    /*[Header("Jugador")]
    [SerializeField] private int velocidad;

    private Vector3 movim = new Vector3(1, 0, 0);
    private Rigidbody2D rigbody;*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start");
        _playerInput = GetComponent<PlayerInput>();
        //rigbody = GetComponent<Rigidbody2D>();
        //transform.Translate(new Vector3(10, 1));
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();

        Debug.Log("Input: " + _input);

        transform.position += new Vector3(_input.x,_input.y,0) * Time.deltaTime * _playerSpeed;
        //rigbody.linearVelocity = movim * velocidad;
    }

    private void ReadInput() {
        _input = _playerInput.actions["Move"].ReadValue<Vector2>();

    }
}
