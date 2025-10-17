using DG.Tweening;
using UnityEngine;

namespace Objects.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public class DoorCloser : MonoBehaviour, IActivatable, IDeactivatable
    {
        private Collider2D col;

        [Header("||===== Parameters =====||")]
        [SerializeField] private Transform doorTransform;
        [SerializeField] private Transform closedPosition;
        [SerializeField] private float closeDuration;
        [SerializeField] private float shakeDuration;
        [SerializeField] private float closeShakeIntensity;
        private Vector2 initialPos;

        public bool showGizmos;

        private void Awake()
        {
            col = GetComponent<Collider2D>();

            initialPos = doorTransform.position;
        }

        private void Start()
        {
            // Começa desativada
            Deactivate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                col.enabled = false;

                doorTransform.DOMove(closedPosition.position, closeDuration).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    doorTransform.DOShakePosition(shakeDuration, new Vector3(closeShakeIntensity, 0f, 0f), 10, 0);
                });
            }
        }

        // ATIVAR a porta = Abrir
        public void Activate()
        {
            doorTransform.position = initialPos;
            col.enabled = true;
        }

        // DESATIVAR a porta = Fechar
        public void Deactivate()
        {
            doorTransform.position = closedPosition.position;
            col.enabled = false;
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(closedPosition.position, doorTransform.localScale);
        }
    }
}