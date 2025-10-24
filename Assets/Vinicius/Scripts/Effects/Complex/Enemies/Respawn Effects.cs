using Effects.Simple;
using UnityEngine;

namespace Effects.Complex.Enemies
{
    public class RespawnEffects : MonoBehaviour
    {
        public static RespawnEffects Instance;

        [Header("Objects")]
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

            //Remove o efeito de dissolveS
            spriteDissolve.RemoveEffect(renderer, dissolveDuration);

            finishedPlaying = true;
        }
    }
}