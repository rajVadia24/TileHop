using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerTest : MonoBehaviour
{
    public static BallControllerTest inst;

    private float _animation;

    private Vector3 _startPosition, _endPosition;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        ParaBola();
    }

    void ParaBola()
    {
        _animation += Time.deltaTime;
        _animation = _animation % 2f;

        transform.position = MathParabola.Parabola(_startPosition, _endPosition, 2, _animation / 2f);
    }

    public void GetEndPosition(Vector3 endPosition)
    {
        _endPosition = endPosition;        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            _startPosition = transform.position;
        }
    }
}
