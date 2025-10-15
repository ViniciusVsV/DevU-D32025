using DG.Tweening;
using UnityEngine;

namespace Objects.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public class DoorCloser : MonoBehaviour
    {
        private Collider2D col;

        [Header("||===== Parameters =====||")]
        [SerializeField] private Transform doorTransform;
        [SerializeField] private Transform doorClosedPosition;
        [SerializeField] private float closeDuration;
        [SerializeField] private float shakeDuration;
        [SerializeField] private float closeShakeIntensity;

        public bool showGizmos;

        private void Awake()
        {
            col = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                col.enabled = false;

                doorTransform.DOMove(doorClosedPosition.position, closeDuration).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    doorTransform.DOShakePosition(shakeDuration, new Vector3(closeShakeIntensity, 0f, 0f), 10, 0);
                });
            }
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(doorClosedPosition.position, doorTransform.localScale);
        }
    }
}