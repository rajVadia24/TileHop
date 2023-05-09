using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    public static BallController inst;

    [SerializeField] Camera _mainCamera;
    [SerializeField] private float _flightTime = 2;
    [SerializeField] private float _maxHeight = 1;

    private Vector3 _startPosition, _endPosition, _controlPoint;

    private float _time;

    private void Awake()
    {
        inst = this;
    }

    private void FixedUpdate()
    {
        MoveBall();
        BallJump();
    }

    private void MoveBall()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 input = _mainCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, _mainCamera.transform.position.z));            
            transform.position = new Vector3(input.x, transform.position.y, transform.position.z);
        }
    }

    private void BallJump()
    {
        if (_time < _flightTime)
        {
            float t = _time / _flightTime;
            Vector3 position = Mathf.Pow(1 - t, 2) * _startPosition + 2 * t * (1 - t) * _controlPoint + Mathf.Pow(t, 2) * _endPosition;
            transform.position = new Vector3(transform.position.x, position.y, transform.position.z);
            _time += Time.deltaTime;
        }
    }

    public void SetControlPoint(Vector3 controlPoint)
    {
        _endPosition = controlPoint;
        _controlPoint = _startPosition + (_endPosition - _startPosition) / 2 + Vector3.up * _maxHeight;
        _time = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            Debug.Log("Tile Hit");
            _startPosition = transform.position;            
        }
    }
}
