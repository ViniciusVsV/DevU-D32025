using UnityEngine;

namespace Characters.Enemies.Hound
{
    public class PlayerDetector : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        [SerializeField] StateController houndController;
        [SerializeField] LayerMask terrainLayers;

        private Transform playerTransform;
        private Vector2 playerVector;

        public bool showGizmos;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerTransform = other.transform;
                houndController.playerTransform = playerTransform;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                playerTransform = null;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerVector = playerTransform.position - transform.position;

                if (!Physics2D.Raycast(transform.position, playerVector.normalized, playerVector.magnitude, terrainLayers))
                    houndController.isAggroed = true;
            }
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos)
                return;

            Gizmos.color = Color.green;
            if (playerTransform != null)
                Gizmos.DrawLine(transform.position, playerTransform.position);
        }
    }
}