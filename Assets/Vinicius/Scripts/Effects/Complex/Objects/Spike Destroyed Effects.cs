using Effects.Simple;
using UnityEngine;

namespace Effects.Complex.Objects
{
    public class SpikeDestroyedEffects : MonoBehaviour
    {
        public static SpikeDestroyedEffects Instance;

        [Header("Objects")]
        [SerializeField] private ParticleSystem particlesPrefab;
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

        public void ApplyEffects(Vector2 position, Renderer renderer)
        {
            finishedPlaying = false;

            renderer.material = dissolveMaterial;

            spriteDissolve.ApplyEffect(renderer, dissolveDuration);

            ParticleSystem particles = Instantiate(particlesPrefab, transform);
            particles.transform.position = position;
            particles.Play();

            finishedPlaying = true;
        }
    }
}