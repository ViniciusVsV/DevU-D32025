using System;
using UnityEngine;

namespace Objects.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public class CheckpointCollider : MonoBehaviour
    {
        public int checkpointId;

        private Collider2D col;

        public static event Action OnCheckpointReached;

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

                OnCheckpointReached?.Invoke();
            }
        }
    }
}