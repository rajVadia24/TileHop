using System.Collections;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private float animationDuration;// = 0.2f;
    [SerializeField] private float targetScaleMultiplier;// = 1.2f;    
    [SerializeField] private float colorChangeDuration;
    [SerializeField] private Color constantColor = Color.black;
    [SerializeField] private Color[] tileColors = { Color.blue, Color.green, Color.magenta, Color.yellow, Color.red };

    private Material _mat;

    private Color ColorChange;    

    private Vector3 originalScale;
    private Color originalColor;
    private Coroutine colorChangeCoroutine;

    private const string _player = "Player";

    private GameObject NextTilePosition()
    {
        int lenght = SpawnManager.Inst.SpawnedList.Count;

        for (int i = 0; i < lenght; i++)
        {
            if (SpawnManager.Inst.SpawnedList[i] == _parent)
            {
                GameObject nextTile = SpawnManager.Inst.SpawnedList[i + 1];
                return nextTile;
            }
        }
        return null;
    }

    private void Start()
    {
        _mat = GetComponent<MeshRenderer>().materials[1];
        originalScale = transform.localScale;
        originalColor = _mat.color;
    }

    private void ReUseTile()
    {
        _parent.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.GetComponent<BallController>()._tag == Tags.Player)
        {
            Debug.Log("COLLISON");
      

            ScoreManager.Inst.Score += 1;
            ScoreManager.Inst._scoreText.text = "Score: " + ScoreManager.Inst.Score;
            BallController.Inst?.GetNextTilePosition(NextTilePosition().transform.position);
            SpawnManager.Inst.SpawnedList.Remove(_parent);
            Invoke(nameof(ReUseTile), 2f);
            SpawnManager.Inst.OnSpawnTile.Invoke();
        }
    }

    public void TileEffects()
    {
        StartCoroutine(ColorChangeAnimation());
        StartCoroutine(ResizeAnimation());
    }

    private IEnumerator ResizeAnimation()
    {
        float timer = 0f;
        Vector3 targetScale = originalScale * targetScaleMultiplier;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / animationDuration);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }
        transform.localScale = originalScale;
    }    

    private IEnumerator ColorChangeAnimation()
    {
        Color startColor = constantColor;
        Color endColor = tileColors[Random.Range(0, tileColors.Length)];

        float timer = 0f;
        //colorChangeDuration = 0.5f;

        while (timer < colorChangeDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / colorChangeDuration);
            ColorChange = Color.Lerp(startColor, endColor, t); 
            _mat.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        _mat.color = originalColor;
    }
}

public enum Tags
{
    Player,
}