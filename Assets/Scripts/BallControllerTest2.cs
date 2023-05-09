using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerTest2 : MonoBehaviour
{
    private Rigidbody _ballRigidbody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Camera _mainCamera;

    private void Start()
    {
        _ballRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveBall();
        //BallJump();        
    }
    private void BallJump()
    {
         _ballRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            BallJump();
            Debug.Log("Tile Hit");
        }
    }

    private void MoveBall()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 input = _mainCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, _mainCamera.transform.position.z));
            transform.position = new Vector3(input.x, transform.position.y, transform.position.z);
        }
    }
}
