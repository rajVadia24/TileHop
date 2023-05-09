using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private float _tileSpeed = 5;

    private void Update()
    {
        if (!IsInvoking(nameof(ReUseTile)))
            Invoke(nameof(ReUseTile), 5);
    }

    private void FixedUpdate()
    {
        MoveTile();
    }

    private void MoveTile()
    {
        transform.Translate(_tileSpeed * Time.deltaTime * Vector3.forward);
    }

    private void ReUseTile()
    {
        gameObject.SetActive(false);
    }
}
