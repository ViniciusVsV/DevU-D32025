using Effects.Simple.AfterImage;
using UnityEngine;

namespace Effects.Complex.Player
{
    public class MovementEffects : MonoBehaviour
    {
        public static MovementEffects Instance;

        [Header("Objects")]
        private AfterImagesManager afterImagesManager;

        [Header("Jump Parameters")]
        [SerializeField] private ParticleSystem jumpParticles;
        [SerializeField] private ParticleSystem doubleJumpParticles;
        [SerializeField] private ParticleSystem wallJumpParticles;

        [Header("Land Parameters")]
        [SerializeField] private ParticleSystem landParticles;

        [Header("Fast Run Parameters")]
        [SerializeField] private ParticleSystem fastRunParticles;

        private AudioController audioController;
        private bool fastRunEffectsActive;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            afterImagesManager = FindFirstObjectByType<AfterImagesManager>();

            audioController = AudioController.Instance;
        }

        public void ApplyJumpEffects(Vector2 position)
        {
            //Invoca partículas
            jumpParticles.transform.position = position;
            jumpParticles.Play();

            //Chama efeito sonoro
            audioController.PlayPlayerJumpSFX();
        }

        public void ApplyDoubleJumpEffects(Vector2 position)
        {
            //Invoca partículas
            doubleJumpParticles.transform.position = position;
            doubleJumpParticles.Play();

            //Chama efeito sonoro
            audioController.PlayPlayerJumpSFX();
        }

        public void ApplyWallJumpEffects(Vector2 position)
        {
            //Invoca partículas
            wallJumpParticles.transform.position = position;
            wallJumpParticles.Play();

            //Chama efeito sonoro
            audioController.PlayPlayerJumpSFX();
        }

        public void ApplyLandEffects(Vector2 position)
        {
            //Invoca partículas
            landParticles.transform.position = position;
            landParticles.Play();

            audioController.PlayPlayerLandSFX();
        }

        public void ApplyFastRunEffects(Transform playerTransform, SpriteRenderer playerSprite)
        {
            if (fastRunEffectsActive)
                return;

            fastRunEffectsActive = true;

            //Invoca partículas
            //fastRunParticles.transform.position = position;
            //fastRunParticles.Play();

            //Ativa afetimages
            afterImagesManager.StartAfterImages(playerTransform, playerSprite);
        }
        public void RemoveFastRunEffects(Transform playerTransform)
        {
            fastRunEffectsActive = false;

            //fastRunParticles.Stop();
            //Desativa afterimgaes
            afterImagesManager.StopAfterImages(playerTransform);
        }

        public void ApplyDashEffects()
        {
            audioController.PlayPlayerDashSFX();
        }

        public void ApplyRunSFX()
        {
            audioController.PlayPlayerWalkSFX();
        }
    }
}