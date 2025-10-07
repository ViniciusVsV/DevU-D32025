using UnityEngine;

namespace Objects.Obstacles
{
    public class Laser : MonoBehaviour, IRythmSyncable
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private GameObject rayObject;

        [Header("||===== Parameters =====||")]
        [SerializeField] private int nActiveBeats;
        [SerializeField] private int nInactiveBeats;

        private int cycleLength => nActiveBeats + nInactiveBeats;
        private int beatCounter;

        public bool shouldBeActive;
        public bool isActive;

        public void RespondToBeat()
        {
            beatCounter = (beatCounter + 1) % cycleLength;

            shouldBeActive = beatCounter < nActiveBeats;

            if (shouldBeActive != isActive)
                ToggleLaser();
        }

        public void ToggleLaser()
        {
            isActive = !isActive;

            rayObject.SetActive(isActive);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            rayObject.SetActive(isActive);
        }
#endif
    }
}