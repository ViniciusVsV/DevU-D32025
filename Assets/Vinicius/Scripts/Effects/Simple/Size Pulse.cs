using DG.Tweening;
using UnityEngine;

public class SizePulse : MonoBehaviour, IRythmSyncable
{
    [SerializeField] private float pulseSize;
    private float beatLength;

    private void Start()
    {
        beatLength = BeatController.Instance.GetBeatLength();
    }

    private void OnEnable() { BeatInterval.OnOneBeatHappened += RespondToBeat; }
    private void OnDisable() { BeatInterval.OnOneBeatHappened -= RespondToBeat; }

    public void RespondToBeat()
    {
        transform.DOKill();

        transform.localScale = Vector3.one * pulseSize;

        transform.DOScale(Vector3.one, beatLength);
    }
}