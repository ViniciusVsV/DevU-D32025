using UnityEngine;

namespace Effects.Complex.Enemies
{
    public class NoteHitEffects : MonoBehaviour
    {
        public static NoteHitEffects Instance;

        [Header("Objects")]

        [Header("Parameters")]

        [Header("Controller Rumble Parameters")]

        [Header("Control Booleans")]
        public bool finishedPlaying;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
        }

        public void ApplyEffects(Transform noteTransform)
        {
            // Invoca efeito sonoro

            // Invoca efeito visual da nota
        }
    }
}