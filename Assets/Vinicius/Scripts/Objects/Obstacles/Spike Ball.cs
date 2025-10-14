using UnityEngine;
using UnityEngine.Splines;

namespace Objects.Obstacles
{
    public class SpikeBall : MonoBehaviour, IRythmSyncable
    {
        [SerializeField] private SplineAnimate splineAnimate;
        private float beatLength;

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();

            splineAnimate.Duration = beatLength;
        }

        public void RespondToBeat()
        {
            splineAnimate.Restart(false);
            splineAnimate.Play();
        }
    }
}