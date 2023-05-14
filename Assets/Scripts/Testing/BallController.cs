using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    public static BallController Inst;

    private InputManager _inputManager;

    [SerializeField] private float _mouseSpeed;
    [SerializeField] private float _height;    
    [SerializeField] private float _constantSpeed;
    [SerializeField] private Camera _mainCamera;

    private Vector3 _startPosition;
    private Vector3 _endPosition;    

    private float _startTime;
    private float _timeFraction;
    private float _distance;

    private bool isPlaying;    
    private bool isColliding;    

    public float Distance { get => _distance; }
    public float StartTime { get => _startTime; }
    public float ConstantSpeed { get => _constantSpeed; set => _constantSpeed = value; }

    private void Awake()
    {
        Inst = this;
        _inputManager = new();
    }

    private void OnEnable()
    {
        _inputManager.Player.Enable();
        _inputManager.Player.Movement.performed += ControlBallViaInput;
        _inputManager.Player.StartGame.started += StartGame;
    }

    private void OnDisable()
    {
        _inputManager.Player.Disable();
    }

    private void Update()
    {
        GameOver();    
    }

    private void FixedUpdate()
    {           
        if(isPlaying)
            MoveTowardsTile();
    }

    private void StartGame(InputAction.CallbackContext context)
    {
        _inputManager.Player.StartGame.Disable();
        isPlaying = true;
        _startTime = Time.time;
        //_endPosition = ObjectPooling.Inst.ListOfObjects[0].transform.position;
        _endPosition = ObjectPooling.Inst.ListOfObjects[0].transform.position;
    }

    private void ControlBallViaInput(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();

        float horizontalMovement = -input * _mouseSpeed * Time.deltaTime;

        // Move the ball horizontally
        transform.Translate(horizontalMovement, 0f, 0f);
    }    

    public void GetNextTilePosition(Vector3 nextTile)
    {
        _startTime = Time.time;
        _startPosition = transform.position;
        _endPosition = nextTile;
        //Debug.Log("TestBallTile: " + _endPosition);
    }

    private void MoveTowardsTile()
    {
        _distance = Vector3.Distance(_startPosition, _endPosition);

        if (_distance < 2)
        {
            _height = 0.7f;
            if (_distance < 1) { _height = 0.6f; }
        }
        else { _height = 1f; }

        float totalTime = _distance / ConstantSpeed;

        _timeFraction = Mathf.Clamp01((Time.time - _startTime) / totalTime);
        float currentHeight = _height * (_timeFraction - _timeFraction * _timeFraction);
        Vector3 moveTowardsTile = Vector3.Lerp(_startPosition, _endPosition, _timeFraction) + Vector3.up * currentHeight;
        transform.position = new Vector3(transform.position.x, moveTowardsTile.y, moveTowardsTile.z);

        //Debug.Log("EndPosition: " + _endPosition);
        if (transform.position == _endPosition)
        {
            _startPosition = _endPosition;
        }
    }

    private void GameOver()
    {
        if(_timeFraction == 1 && isColliding == false)
        {
            Debug.LogError("Game Over");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }  
}