using UnityEngine;

namespace Characters.Enemies.VynilDisc
{
    public class PlayerDetector : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private NoteSequence noteSequence;
        [SerializeField] StateController vynilDiscController;
        [SerializeField] LayerMask terrainLayers;

        private Transform playerTransform;
        private Vector2 playerVector;

        public bool showGizmos;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerTransform = other.transform;
                vynilDiscController.playerTransform = playerTransform;
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
                {
                    vynilDiscController.isAggroed = true;
                    
                    if(!vynilDiscController.isDead)
                        noteSequence.Activate();
                }
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