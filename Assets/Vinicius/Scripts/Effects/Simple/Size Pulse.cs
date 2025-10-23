using DG.Tweening;
using UnityEngine;

public class SizePulse : MonoBehaviour, IRythmSyncable
{
    [SerializeField] private float pulseSize;
    private float beatLength;

    private Tween tween;

    private void Start()
    {
        beatLength = BeatController.Instance.GetBeatLength();
    }

    public void RespondToBeat()
    {
        tween?.Kill();

        transform.localScale = Vector3.one * pulseSize;

        tween = transform.DOScale(Vector3.one, beatLength).SetUpdate(true);
    }
}