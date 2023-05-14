using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _ball;

    private float _offSet;

    private void Start()
    {
        _offSet = transform.position.z;        
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _ball.position.z + _offSet);
    }
}
