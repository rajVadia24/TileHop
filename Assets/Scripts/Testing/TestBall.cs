using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestBall : MonoBehaviour
{
    public static TestBall inst;

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
        MoveBall();

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
        EndPosition = ObjectPooling.inst.listOfObjects[0].transform.position;
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

    private void MoveBall()
    {
        if (Input.GetMouseButton(0))
        {
            if( 0 > Input.GetAxis("Mouse X"))
            {
                transform.Translate(_mouseSpeed * Time.deltaTime * Vector2.right);
            }
            else if ( 0 < Input.GetAxis("Mouse X"))
            {
                transform.Translate(_mouseSpeed * Time.deltaTime * Vector2.left);
            }
        }
    }   
}
