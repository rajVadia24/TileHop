using System.Collections.Generic;
using UnityEngine;

public class ManualBeatDetector : MonoBehaviour
{
    [SerializeField] private List<float> _clickTimes = new();

    bool start;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _clickTimes.Add(Time.time);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            start = true;
        }

        if (start)
        {
            GetClickTimes();
        }
    }

    public void GetClickTimes()
    {
        for (int i = 0; i < _clickTimes.Count; i++)
        {
            if (i == 0)
            {
                Debug.Log("Click at time: " + _clickTimes[i]);
            }
            else
            {
                float timeDiff = _clickTimes[i] - _clickTimes[i - 1];
                Debug.Log("Time elapsed from previous click: " + timeDiff);
                Debug.Log("Click at time: " + _clickTimes[i]);
            }
        }
    }
}
