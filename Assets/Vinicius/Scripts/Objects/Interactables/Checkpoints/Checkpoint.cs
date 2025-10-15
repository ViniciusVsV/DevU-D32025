using UnityEngine;

namespace Objects.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public class Checkpoint : MonoBehaviour
    {
        public int checkpointId;
        public Transform spawnPoint;

        private Collider2D col;

        private void Awake()
        {
            col = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                col.enabled = false;

                PlayerPrefs.SetInt("checkpointId", checkpointId);
            }
        }
    }
}