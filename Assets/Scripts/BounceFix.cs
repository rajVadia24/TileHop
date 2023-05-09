using UnityEngine;

public class BounceFix : MonoBehaviour
{
    private Vector3 _playerPosition;

    private void FixedUpdate()
    {
        _playerPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.position = _playerPosition;
    }
}