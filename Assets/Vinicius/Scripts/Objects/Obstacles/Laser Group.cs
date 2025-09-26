using UnityEngine;

namespace Objects.Obstacles
{
    public class LaserGroup : MonoBehaviour, IRythmSyncable
    {
        [SerializeField] private Laser[] lasers;

        private int current;
        private int previous;

        public void RespondToBeat()
        {
            lasers[current].ToggleLaser();
            if (previous != current)
                lasers[previous].ToggleLaser();

            previous = current;

            current++;
            current = current >= lasers.Length ? 0 : current;
        }
    }
}