using UnityEngine;

namespace Effects.Complex.Player
{
    public class MovementEffects : MonoBehaviour
    {
        public static MovementEffects Instance;

        [Header("Jump Parameters")]
        [SerializeField] private ParticleSystem jumpParticles;
        [SerializeField] private ParticleSystem doubleJumpParticles;
        [SerializeField] private ParticleSystem wallJumpParticles;

        [Header("Land Parameters")]
        [SerializeField] private ParticleSystem landParticles;

        [Header("Fast Run Parameters")]
        [SerializeField] private ParticleSystem fastRunParticles;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {

        }

        public void ApplyJumpEffects(Vector2 position)
        {
            //Invoca partículas
            jumpParticles.transform.position = position;
            jumpParticles.Play();

            //Chama efeito sonoro

        }

        public void ApplyDoubleJumpEffects(Vector2 position)
        {
            //Invoca partículas
            doubleJumpParticles.transform.position = position;
            doubleJumpParticles.Play();

            //Chama efeito sonoro

        }

        public void ApplyWallJumpEffects(Vector2 position)
        {
            //Invoca partículas
            wallJumpParticles.transform.position = position;
            wallJumpParticles.Play();

            //Chama efeito sonoro
        }

        public void ApplyLandEffects(Vector2 position)
        {
            //Invoca partículas
            landParticles.transform.position = position;
            landParticles.Play();

            //Chama efeito sonoro

        }

        public void ApplyFastRunEffects(Vector2 position)
        {
            //Invoca partículas
            fastRunParticles.transform.position = position;
            fastRunParticles.Play();
        }
    }
}