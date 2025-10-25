using Effects.Simple;
using UnityEngine;

namespace Effects.Complex.Objects
{
    public class SpikeDestroyedEffects : MonoBehaviour
    {
        public static SpikeDestroyedEffects Instance;

        [Header("Objects")]
        [SerializeField] private Material dissolveMaterial;
        private SpriteDissolve spriteDissolve;

        [Header("Parameters")]
        [SerializeField] private float dissolveDuration;

        [Header("Control Booleans")]
        public bool finishedPlaying;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            spriteDissolve = FindFirstObjectByType<SpriteDissolve>();
        }

        public void ApplyEffects(Renderer renderer)
        {
            finishedPlaying = false;

            renderer.material = dissolveMaterial;

            spriteDissolve.ApplyEffect(renderer, dissolveDuration);

            finishedPlaying = true;
        }

        public void RemoveEffects(Renderer renderer)
        {
            spriteDissolve.RemoveEffect(renderer, 0);
        }
    }
}