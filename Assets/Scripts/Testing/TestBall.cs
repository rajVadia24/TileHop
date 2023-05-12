using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class TestBall : MonoBehaviour
{
    public static TestBall Inst;

    private InputManager _inputManager;

    [SerializeField] private float _mouseSpeed;
    [SerializeField] private float _height;
    [SerializeField] private float _duration;
    [SerializeField] private Camera _mainCamera;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _nextPosition;

    private float _startTime;

    private bool isPlaying;
    public bool isStopped;
    private bool isColliding;

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
        _endPosition = ObjectPooling.Inst.ListOfObjects[0].transform.position;
    }

    private void ControlBallViaInput(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();

        if (0 > input)
            transform.Translate(_mouseSpeed * Time.deltaTime * Vector2.right);

        else if (0 < input)
            transform.Translate(_mouseSpeed * Time.deltaTime * Vector2.left);
    }

    public void GetNextTilePosition(Vector3 nextTile)
    {
        _startTime = Time.time;
        _startPosition = transform.position;
        _endPosition = nextTile;
        Debug.Log("TestBallTile: " + _endPosition);
    }

    private void MoveTowardsTile()
    {        
        float timeFraction = Mathf.Clamp01((Time.time - _startTime) / _duration);
        float currentHeight = _height * (timeFraction - timeFraction * timeFraction);
        Vector3 moveTowardsTile = Vector3.Lerp(_startPosition, _endPosition, timeFraction) + Vector3.up * currentHeight;
        transform.position = new Vector3(transform.position.x , moveTowardsTile.y, moveTowardsTile.z);

        Debug.Log("EndPosition: " + _endPosition);
        if (transform.position == _endPosition)
        {
            _startPosition = _endPosition;
        }

        if (timeFraction == 1)        
            isStopped = true;        
        else
            isStopped = false;
    }

    private void GameOver()
    {
        if(isStopped == true && isColliding == false)
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
