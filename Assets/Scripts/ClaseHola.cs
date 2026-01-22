using UnityEngine;
using UnityEngine.InputSystem;

public class ClaseHola : MonoBehaviour
{
    [SerializeField] private int _playerSpeed;
    [SerializeField] private float _smoothTime;
    private Vector2 _input;
    private PlayerInput _playerInput;
    private Vector2 _currentVelocity = Vector2.zero;
    private Vector2 _fixedInput;

    /*[Header("Jugador")]
    [SerializeField] private int velocidad;

    private Vector3 movim = new Vector3(1, 0, 0);*/
    private Rigidbody2D _rigbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start");
        _playerInput = GetComponent<PlayerInput>();
        _rigbody = GetComponent<Rigidbody2D>();
        //transform.Translate(new Vector3(10, 1));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ReadInput();

        Debug.Log("Input: " + _input);

        //transform.position += new Vector3(_input.x,_input.y,0) * Time.deltaTime * _playerSpeed; 

        //_rigbody.linearVelocity = new Vector3(_input.x,_input.y,0) * _playerSpeed;

        //_rigbody.MovePosition(new Vector2 (transform.position.x, transform.position.y) + _input * _playerSpeed * Time.fixedDeltaTime);

        //_rigbody.AddForce(_input * _playerSpeed);
        //Vector2 target = new Vector2(transform.position.x, transform.position.y) + _input * _playerSpeed * Time.fixedDeltaTime

        _fixedInput = Vector2.SmoothDamp(_fixedInput, _input, ref _currentVelocity, _smoothTime);
        _rigbody.MovePosition(_rigbody.position + _fixedInput * _playerSpeed * Time.fixedDeltaTime);
    }

    private void ReadInput() {
        _input = _playerInput.actions["Move"].ReadValue<Vector2>();

    }
}
