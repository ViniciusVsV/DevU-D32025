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

        public void ApplyEffect(CinemachineImpulseSource source = null)
        {
            if (source != null)
                source.GenerateImpulse();

            else
                baseSource.GenerateImpulse();
        }
    }
}