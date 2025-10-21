using System;
using UnityEngine;

[System.Serializable]
public class BeatInterval
{
    [SerializeField] private GameObject[] syncedObjects;
    private IRythmSyncable rythmSyncable;

    [SerializeField] private float noteLength;
    private float lastInterval;

    public static event Action OnOneBeatHappened; // Evento apenas a cada uma batida (a cada duas batidas, por exemplo, não ocorre)

    public float GetBeatLength(float bpm)
    {
        return 60f / (bpm * noteLength);
    }

    public void CheckInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);

            if (noteLength == 1)
                OnOneBeatHappened?.Invoke();

            foreach (var syncedObject in syncedObjects)
            {
                if (syncedObject != null)
                {
                    rythmSyncable = syncedObject.GetComponent<IRythmSyncable>();
                    rythmSyncable?.RespondToBeat();
                }
            }
        }
    }
}