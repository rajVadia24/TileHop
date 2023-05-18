using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _ball;
    [SerializeField] private GameObject _baground;

    private float _offSet;
    private float _bagroundOffset;

    private void Start()
    {
        _offSet = transform.position.z;
        _bagroundOffset = _baground.transform.position.z;
    }
    
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _ball.position.z + _offSet);
        _baground.transform.position = new Vector3(_baground.transform.position.x, _baground.transform.position.y, _bagroundOffset + _ball.position.z);
    }
    
}
