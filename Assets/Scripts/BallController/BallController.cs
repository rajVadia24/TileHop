using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    public static BallController Inst;

    private InputManager _inputManager;

    public Tags _tag;

    [SerializeField] private float _mouseSpeed;
    [SerializeField] private float _height;    
    [SerializeField] private float _constantSpeed;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _startTile;
    [SerializeField] private Slider _sensitivitySlider;

    private Vector3 _startPosition;
    private Vector3 _endPosition;    

    private float _startTime;
    private float _timeFraction;
    private float _distance;

    private bool isPlaying;    
    private bool isColliding;

    private AudioTrack _songName;

    public float Distance { get => _distance; }
    public float StartTime { get => _startTime; }
    public float ConstantSpeed { get => _constantSpeed; set => _constantSpeed = value; }

    public Material CloneMaterial;

    private Action OnMovingToTile;

    private void Awake()
    {
        Inst = this;
        _inputManager = new();
    }
   
    private void Start()
    {
        OnMovingToTile += MoveTowardsTile;        
    }

    private void OnEnable()
    {        
        _inputManager.Player.Movement.performed += ControlBallViaInput;
        _inputManager.Player.StartGame.started += StartGame;

        GameStateManager.OnGameStateChange += StateChange;
    }

    private void OnDisable()
    {
        _inputManager.Player.Movement.performed -= ControlBallViaInput;
        _inputManager.Player.StartGame.started -= StartGame;

        GameStateManager.OnGameStateChange -= StateChange;
    }

    private void StateChange(GameStates gs)
    {
        switch (gs)
        {
            case GameStates.GamePlay:
                Debug.Log("--GamePlay--");
                _inputManager.Player.Enable();
                break;

            case GameStates.GameOver:
                Debug.Log("--GameOver--");
                isPlaying = false;                
                _inputManager.Player.Disable();
                break;

            case GameStates.HomeScreen:
                Debug.Log("Home");
                break;
        }        
    }

    private void FixedUpdate()
    {
        if (isPlaying)
            OnMovingToTile?.Invoke();
    }

    private void RotateBall()
    {
        transform.RotateAround(transform.position, -transform.right, Time.deltaTime * 90f);
    }

    private void StartGame(InputAction.CallbackContext context)
    {
        Debug.Log("STARTED GAME");
        _inputManager.Player.StartGame.Disable();
        isPlaying = true;
        _startTime = Time.time;
        _startPosition = transform.position;
        _endPosition = SpawnManager.Inst.SpawnedList[0].transform.position;
        AudioManager.Inst.PlaySound(_songName);
    }

    public void SoundToPlay(AudioTrack song)
    {
        _songName = song;
    }    

    private void ControlBallViaInput(InputAction.CallbackContext context)
    {
        
        Debug.Log("Input Working");
        Vector2 input = context.ReadValue<Vector2>();

        float horizontalMovement = -input.x * 0.04f * Time.deltaTime;

        Vector3 newPosition = transform.position + new Vector3(horizontalMovement, 0f, 0f);        
        Vector3 viewportPosition = _mainCamera.WorldToViewportPoint(newPosition);
        
        float clampedX = Mathf.Clamp01(viewportPosition.x);
        float clampedY = Mathf.Clamp01(viewportPosition.y);
        
        Vector3 clampedPosition = _mainCamera.ViewportToWorldPoint(new Vector3(clampedX, clampedY, viewportPosition.z));

        transform.position = clampedPosition;
    }

    public void GetNextTilePosition(Vector3 nextTile)
    {
        _startTime = Time.time;
        _startPosition = transform.position;
        _endPosition = nextTile;        
    }

    private void MoveTowardsTile()
    {
        RotateBall();
        Debug.Log("MOVING");
        _distance = Vector3.Distance(_startPosition, _endPosition);

        if (_distance < 2)
        {
            _height = 1.3f;
            if (_distance < 1) { _height = 1.1f; }
        }
        else { _height = 1.7f; }

        float totalTime = _distance / ConstantSpeed;

        _timeFraction = Mathf.Clamp01((Time.time - _startTime) / totalTime);
        float currentHeight = _height * (_timeFraction - _timeFraction * _timeFraction);
        Vector3 moveTowardsTile = Vector3.Lerp(_startPosition, _endPosition, _timeFraction) + Vector3.up * currentHeight;
        transform.position = new Vector3(transform.position.x, moveTowardsTile.y, moveTowardsTile.z);
        
        if (transform.position == _endPosition)
        {
            _startPosition = _endPosition;
        }

        if (_timeFraction == 1 && !isColliding)
        {
            GameOver();
        }
    }
    
    private void GameOver()
    {        
        Debug.LogError("Game Over");        
        AudioManager.Inst.StopSound();
        ScreenManager.Inst.ShowNextScreen(ScreenType.GameOverPanel);
        ScreenManager.Inst.GameOverObj.DisplayScore();        
    }

    public void Restart()
    {
        Debug.Log("Restart");
        ConstantSpeed = 1.5f;                   
        _timeFraction = 0;
        _startTile.transform.position = Vector3.zero;
        Vector3 ballStartPosition = _startTile.transform.position;
        transform.position = ballStartPosition;          
        SpawnManager.Inst.ResetSpawnPoints();
        SpawnManager.Inst.ResetSpawnedTiles();
        SpawnManager.Inst.OnSpawnTile.Invoke();
        ScoreManager.Inst.Score = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {        
        isColliding = true;
        TileController tileClone = collision.gameObject.GetComponent<TileController>();
        tileClone?.TileEffects();
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }


}