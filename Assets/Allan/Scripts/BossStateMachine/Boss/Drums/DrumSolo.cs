using UnityEngine;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using Unity.Cinemachine;
using System.Collections.Generic;

public class DrumSolo : MonoBehaviour , IRythmSyncable
{
    [Header("-----Solo Settings-----")]
    [SerializeField] public float soloDuration;

    [Header("-----Ground----")]
    [SerializeField] public Transform groundTransform;
    [SerializeField] public Transform originalPosition;
    
    [SerializeField] public Ease easeUp;
    [SerializeField] public Ease easeDown;
    [SerializeField] public Transform bouncePosition;
    [SerializeField] public float upDuration;

    [Header("-----Ground HitBox-----")]
    [SerializeField] public BoxCollider2D boxCollider;
    [SerializeField] public float hitboxActiveTime;
    [Range(0, 1)][SerializeField] private float beatPercentage;
    public ParticleSystem ceilingFX;

    private CinemachineImpulseSource impulseSource;

    private int beatCounter;
    private float beatLength;
    public bool isSoloing;

    public void StartDrumSolo()
    {
        
    }

    private void Start()
    {
        beatLength = BeatController.Instance.GetBeatLength();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Activate()
    {
        isSoloing = true;
    }

    public void Deactivate()
    {
        isSoloing = false;
    }

    public void RespondToBeat()
    {
        beatCounter = (beatCounter + 1) % 4;

        if (beatCounter == 0 && isSoloing == true)
        {
            StartCoroutine(EndDrumSolo());
            groundTransform.DOKill();
            impulseSource.GenerateImpulse();
            ceilingFX.Play();

            var sequence = DOTween.Sequence();
            sequence.Append(groundTransform.DOMove(bouncePosition.position, beatLength * beatPercentage).SetEase(easeUp)
                .OnComplete(() => { boxCollider.enabled = false; })
            );
            sequence.Append(groundTransform.DOMove(originalPosition.position, beatLength * 3).SetEase(easeDown));
            
        }
    }

    private IEnumerator ActivateHitbox()
    {
        boxCollider.enabled = true;
        yield return new WaitForSeconds(hitboxActiveTime);
        boxCollider.enabled = false;
    }

    private IEnumerator EndDrumSolo()
    {
        yield return new WaitForSeconds(soloDuration);
        Deactivate();
    }

}