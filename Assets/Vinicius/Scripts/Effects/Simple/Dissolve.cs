using UnityEngine;

namespace Effects.Simple
{
    public class Dissolve : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private Material shaderMaterial;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float duration;

        public bool isPlaying;

        public void ApplyEffect()
        {
            
        }
    }
}