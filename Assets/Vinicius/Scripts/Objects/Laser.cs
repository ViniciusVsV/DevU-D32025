using UnityEngine;

public class Laser : MonoBehaviour, IRythmSyncable
{
    [SerializeField] private int nActiveBeats;
    [SerializeField] private int nInactiveBeats;

    [SerializeField] private GameObject rayObject;

    private int cycleLength => nActiveBeats + nInactiveBeats;
    private int beatCounter;

    public bool shouldBeActive;
    public bool isActive;

    public void RespondToBeat()
    {
        beatCounter = (beatCounter + 1) % cycleLength;

        shouldBeActive = beatCounter < nActiveBeats;

        if (shouldBeActive != isActive)
            ToggleLaser();
    }

    private void ToggleLaser()
    {
        isActive = !isActive;

        rayObject.SetActive(isActive);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        rayObject.SetActive(isActive);
    }
#endif
}