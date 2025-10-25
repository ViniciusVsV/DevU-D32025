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
    [SerializeField] public Rigidbody2D groundRigidbody;
    [SerializeField] public Transform originalPosition;
    
    [SerializeField] public Ease easeUp;
    [SerializeField] public Ease easeDown;
    [SerializeField] public Transform bouncePosition;
    [SerializeField] public float upDuration = 0.15f;

    [Header("-----Ground HitBox-----")]
    [SerializeField] public BoxCollider2D boxCollider;
    [SerializeField] public float hitboxActiveTime;
    [Range(0, 1)][SerializeField] private float beatPercentage;
    public List<ParticleSystem> ceilingFX;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private CinemachineImpulseSource rumblingSource;
    [SerializeField] private float rumblingDuration;
    [SerializeField] public float downDuration = 0.5f; // descida suave
    private bool isMoving;

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
            rumblingSource.GenerateImpulse();

            foreach (ParticleSystem particles in ceilingFX)
                particles.Play();

            StartCoroutine(MoveGround());
            /*StartCoroutine(EndDrumSolo());
            groundTransform.DOKill();

            rumblingSource.GenerateImpulse();



            foreach (ParticleSystem particles in ceilingFX)
            {
                particles.Play();
            }

            /*var sequence = DOTween.Sequence();
            sequence.Append(groundTransform.DOMove(bouncePosition.position, beatLength * beatPercentage).SetEase(easeUp)
                .OnComplete(() => { StartCoroutine(ActivateHitbox()); })
            );
            
            sequence.Append(groundTransform.DOMove(originalPosition.position, beatLength * 3).SetEase(easeDown));
            */
            //sequence.SetUpdate(UpdateType.Fixed);

            /*groundTransform.position = Vector3.Lerp(originalPosition.position, bouncePosition.position, beatLength);
            groundTransform.position = Vector3.Lerp(bouncePosition.position, originalPosition.position, beatLength / 4);*/
        }
        if (beatCounter == 3)
        {
            StartCoroutine(MoveGround());
        }
        
    }
    private IEnumerator MoveGround()
    {
        isMoving = true;

        // Subida brusca
        Vector3 startPos = groundTransform.position;
        Vector3 targetUp = bouncePosition.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / upDuration;
            groundTransform.position = Vector3.Lerp(startPos, targetUp, t);
            yield return null;
        }

        // Ativa hitbox durante o impacto
        StartCoroutine(ActivateHitbox());

        // Descida suave
        t = 0f;
        Vector3 targetDown = originalPosition.position;

        while (t < 1f)
        {
            t += Time.deltaTime / downDuration;
            // movimento com suavização (ease-out)
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            groundTransform.position = Vector3.Lerp(targetUp, targetDown, smoothT);
            yield return null;
        }

        groundTransform.position = targetDown;
        isMoving = false;
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