using UnityEngine;

public class pulsetest : MonoBehaviour, IRythmSyncable
{
    [SerializeField] private float pulseSize = 1.15f;
    [SerializeField] private float returnSpeed = 5f;
    private Vector3 startSize;

    private void Start()
    {
        startSize = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startSize, Time.deltaTime * returnSpeed);
    }

    public void RespondToBeat()
    {
        transform.localScale = startSize * pulseSize;
    }
}