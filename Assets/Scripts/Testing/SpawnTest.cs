using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SpawnTest : MonoBehaviour
{
    public static SpawnTest Inst;

    [SerializeField] private Transform _tileSpawnPointLeft;
    [SerializeField] private Transform _tileSpawnPointMiddle;
    [SerializeField] private Transform _tileSpawnPointRight;

    public List<GameObject> _SpawnedList;

    private AudioSource _audioSource;
    private float[] _samples;
    private float _lastBeatTime;
    private float _bpm;
    private float _nextBeatTime;

    private void Awake()
    {
        Inst = this;
        _audioSource = GetComponent<AudioSource>();
        _samples = new float[1024];
        _bpm = 120f;
    }

    private void Start()
    {
        _audioSource.Play();
        _nextBeatTime = Time.time;
    }

    private void Update()
    {
        // Check if it's time for the next beat
        if (Time.time >= _nextBeatTime)
        {
            SpawnTile();
            _nextBeatTime += 60f / _bpm;
        }
    }

    private void SpawnTile()
    {
        GameObject tile = ObjectPooling.Inst.ObjectToPool();

        if (tile != null)
        {
            tile.SetActive(true);
            tile.transform.position = RandomSpawnGenerator().position;
            _SpawnedList.Add(tile);
        }
    }

    private Transform RandomSpawnGenerator()
    {
        float randomSpeed = Random.Range(1, 3);
        _tileSpawnPointLeft.position += Vector3.back * randomSpeed;
        _tileSpawnPointMiddle.position += Vector3.back * randomSpeed;
        _tileSpawnPointRight.position += Vector3.back * randomSpeed;

        Transform[] transformArray = { _tileSpawnPointLeft, _tileSpawnPointMiddle, _tileSpawnPointRight };
        int randomTransformPoint = Random.Range(0, transformArray.Length);
        return transformArray[randomTransformPoint];
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        // Calculate the average amplitude of the audio samples
        float sum = 0;
        for (int i = 0; i < data.Length; i++)
        {
            sum += Mathf.Abs(data[i]);
        }
        float amplitude = sum / data.Length;

        // Check if a beat occurred
        if (amplitude > 0.5f && Time.time - _lastBeatTime > 60f / _bpm / 2f)
        {
            _lastBeatTime = Time.time;
            _nextBeatTime = Time.time;
        }
    }
}
