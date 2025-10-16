using System.Collections.Generic;
using Objects.Interactables;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("||===== Objects =====||")]
    [SerializeField] private CheckpointCollider associatedCheckpoint;
    [SerializeField] private Transform spawnPoint;

    [Header("||===== Object Lists =====||")]
    [SerializeField] private List<GameObject> activatableObjects;
    [SerializeField] private List<GameObject> deactivatableObjects;
    [SerializeField] private List<GameObject> restorableObjects;

    private IActivatable activatable;
    private IDeactivatable deactivatable;
    private IRestorable restorable;

    public void ActivateObjects()
    {
        foreach (GameObject obj in activatableObjects)
        {
            if (obj != null)
            {
                activatable = obj.GetComponent<IActivatable>();
                activatable?.Activate();
            }
        }
    }

    public void DeactivateObjects()
    {
        foreach (GameObject obj in deactivatableObjects)
        {
            if (obj != null)
            {
                deactivatable = obj.GetComponent<IDeactivatable>();
                deactivatable?.Deactivate();
            }
        }
    }

    public void RestoreObjects()
    {
        foreach (GameObject obj in restorableObjects)
        {
            if (obj != null)
            {
                restorable = obj.GetComponent<IRestorable>();
                restorable?.Restore();
            }
        }
    }

    public int GetCheckpointId() { return associatedCheckpoint.checkpointId; }
    public Vector2 GetSpawnPoint() { return spawnPoint.position; }
}