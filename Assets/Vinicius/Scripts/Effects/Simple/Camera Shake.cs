using Unity.Cinemachine;
using UnityEngine;

namespace Effects.Simple
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CameraShake : MonoBehaviour
    {
        private CinemachineImpulseSource baseSource;

        private void Awake()
        {
            baseSource = GetComponent<CinemachineImpulseSource>();
        }

        public void ApplyEffect(CinemachineImpulseSource source = null, float force = 0)
        {
            if (source != null)
                source.GenerateImpulse(force);

            else
                baseSource.GenerateImpulse();
        }
    }
}