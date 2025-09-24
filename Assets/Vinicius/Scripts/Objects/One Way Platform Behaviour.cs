using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlatformEffector2D))]
public class OneWayPlatformBehaviour : MonoBehaviour
{
    [SerializeField] private string ignoredLayerName;
    private int ignoredLayer;
    private int baseLayer;

    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private LayerMask noPlayerInteractableLayers;
    private PlatformEffector2D platformEffector2D;

    [SerializeField] private float disabledDuration;
    private Coroutine coroutine;

    private void Awake()
    {
        platformEffector2D = GetComponent<PlatformEffector2D>();

        baseLayer = gameObject.layer;
        ignoredLayer = LayerMask.NameToLayer(ignoredLayerName);
    }

    public void DisableCollision()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        gameObject.layer = ignoredLayer;
        platformEffector2D.colliderMask = noPlayerInteractableLayers;

        yield return new WaitForSeconds(disabledDuration);

        gameObject.layer = baseLayer;
        platformEffector2D.colliderMask = interactableLayers;
    }
}