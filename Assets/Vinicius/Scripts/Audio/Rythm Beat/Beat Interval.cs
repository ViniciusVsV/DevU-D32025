using UnityEngine;

[System.Serializable]
public class BeatInterval
{
    [SerializeField] private GameObject[] syncedObjects;
    private IRythmSyncable rythmSyncable;

    [SerializeField] private float noteLength;
    private float lastInterval;

    public float GetBeatLength(float bpm)
    {
        return 60f / (bpm * noteLength);
    }

    public void CheckInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);

            foreach (var syncedObject in syncedObjects)
            {
                rythmSyncable = syncedObject.GetComponent<IRythmSyncable>();
                rythmSyncable?.RespondToBeat();
            }
        }
    }
}