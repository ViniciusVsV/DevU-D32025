using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Objects.Interactables
{
    public class CheckpointManager : MonoBehaviour
    {
        public static CheckpointManager Instance;

        public List<Checkpoint> checkpoints = new();

        private void Awake()
        {
            Instance = this;

#if UNITY_EDITOR
        PlayerPrefs.DeleteKey("checkpointId");
#endif
        }

        private void Start()
        {
            checkpoints = FindObjectsByType<Checkpoint>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .OrderBy(c => c.checkpointId).ToList();
        }

        public Vector3 GetSpawnPoint() { return checkpoints[PlayerPrefs.GetInt("checkpointId")].spawnPoint.position; }
    }
}