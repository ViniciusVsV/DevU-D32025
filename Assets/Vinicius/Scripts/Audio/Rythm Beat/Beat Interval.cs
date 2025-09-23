using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BeatInterval
{
    [SerializeField] private UnityEvent beatHappened;

    [SerializeField] private float numberBeats;
    private float lastInterval;

    public float GetBeatLength(float bpm)
    {
        return 60f / (bpm * numberBeats);
    }

    public void CheckInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);

            beatHappened.Invoke();
        }
    }
}