using System;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    public static BeatController Instance;

    [SerializeField] private float bpm;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private BeatInterval[] beatIntervals;

    public bool active;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!active)
            return;

        foreach (BeatInterval interval in beatIntervals)
        {
            float sampledTime = musicSource.timeSamples / (musicSource.clip.frequency * interval.GetBeatLength(bpm));

            interval.CheckInterval(sampledTime);
        }
    }

    public float GetBPM() { return bpm; }
    public float GetBeatLength() { return 60f / bpm; }
}