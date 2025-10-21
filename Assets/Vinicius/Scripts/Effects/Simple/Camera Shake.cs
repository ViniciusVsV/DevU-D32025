using Unity.Cinemachine;
using UnityEngine;

namespace Effects.Simple
{
    public class CameraShake : MonoBehaviour
    {
        public void ApplyEffect(CinemachineImpulseSource source, float force, Vector2 position)
        {
            if (position != Vector2.zero)
                source.transform.position = position;

            source.GenerateImpulse(force);
        }
    }
}