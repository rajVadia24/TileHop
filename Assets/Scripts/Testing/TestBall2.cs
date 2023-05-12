using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class TestBall2 : MonoBehaviour
{
    public static TestBall2 inst;

    private InputManager inputManager;

    [SerializeField] private float _mouseSpeed;
    private Vector3 StartPosition;
    private Vector3 EndPosition;
    public Vector3 NextPosition;
    public float Height;
    public float Duration;
    private float startTime;
    public Camera _mainCamera;

    private bool isPlaying;

    private void Awake()
    {
        inst = this;
        inputManager = new();
    }

    private void OnEnable()
    {
        inputManager.Player.Enable();
        inputManager.Player.Movement.performed += MoveBall;
    }

    private void OnDisable()
    {
        inputManager.Player.Disable();
    }

    private void Start()
    {                
        startTime = Time.time;
        StartPosition = transform.position;
        EndPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartGame();
    }

    private void FixedUpdate()
    {        
        //MoveBall2();

        if(isPlaying)
            MoveTowardsTile();
    }

    public void GetNextTilePosition(Vector3 nextTile)
    {
        startTime = Time.time;
        StartPosition = transform.position;
        EndPosition = nextTile;
        Debug.Log("TestBallTile: " + EndPosition);
    }

    private void StartGame()
    {
        isPlaying = true;
        startTime = Time.time;
        EndPosition = ObjectPooling.Inst.ListOfObjects[0].transform.position;
    }

    private void MoveTowardsTile()
    {        
        float timeFraction = Mathf.Clamp01((Time.time - startTime) / Duration);
        float currentHeight = Height * (timeFraction - timeFraction * timeFraction);
        Vector3 moveTowardsTile = Vector3.Lerp(StartPosition, EndPosition, timeFraction) + Vector3.up * currentHeight;
        transform.position = new Vector3(transform.position.x , moveTowardsTile.y, moveTowardsTile.z);

        Debug.Log("EndPosition: " + EndPosition);
        if (transform.position == EndPosition)
        {
            StartPosition = EndPosition;
        }
    }

    private void MoveBall(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();

        if (0 > input)
            transform.Translate(_mouseSpeed * Time.deltaTime * Vector2.right);
        
        else if (0 < input)
            transform.Translate(_mouseSpeed * Time.deltaTime * Vector2.left);                
    }

    private void MoveBallTouch()
    {

    }

    private void MoveBall2()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = _mainCamera.nearClipPlane + 1;

            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);

            transform.position = new Vector3(worldPosition.x, transform.position.y, transform.position.z);
        }
    }
}
