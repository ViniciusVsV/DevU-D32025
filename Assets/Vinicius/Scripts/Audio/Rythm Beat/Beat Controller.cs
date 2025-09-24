using UnityEngine;

public class BeatController : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private BeatInterval[] beatIntervals;

    private void Update() {
        foreach (BeatInterval interval in beatIntervals)
        {
            float sampledTime = musicSource.timeSamples / (musicSource.clip.frequency * interval.GetBeatLength(bpm));

            interval.CheckInterval(sampledTime);
        }
    }
}