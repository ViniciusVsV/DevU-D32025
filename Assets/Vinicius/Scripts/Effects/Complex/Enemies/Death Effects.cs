using System.Collections;
using Effects.Simple;
using Unity.Cinemachine;
using UnityEngine;

namespace Effects.Complex.Enemies
{
    public class DeathEffects : MonoBehaviour
    {
        public static DeathEffects Instance;

        [Header("Objects")]
        [SerializeField] private ParticleSystem particlesPrefab;

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

            //Chama partículas
            var particles = Instantiate(particlesPrefab, transform);
            particles.transform.position = position;
            particles.Play();

            //Chama o efeito de dissolve
            spriteDissolve.ApplyEffect(renderer, dissolveDuration);

            finishedPlaying = true;
        }
    }
}