using Characters.Enemies;
using UnityEngine;

namespace Objects.Obstacles.DestructibleSpike
{
    public class DestructibleSpikesGroup : MonoBehaviour, IActivatable, IDeactivatable, IRestorable
    {
        [Header("||===== Objects =====||")]
        private DestructibleSpike[] destructibleSpikes;
        [SerializeField] private NoteSequence noteSequence;

        [Header("||===== Parameters =====||")]
        public bool isDestroyed;

        private void Awake()
        {
            destructibleSpikes = GetComponentsInChildren<DestructibleSpike>();
        }

        public void Activate()
        {
            // Ativa os colliders e os sprites
            foreach (var spike in destructibleSpikes)
                spike.Activate();
        }

        public void Deactivate()
        {
            noteSequence.Deactivate();
        }

        public void Restore()
        {
            // Ativa os colliders e os sprites
            foreach (var spike in destructibleSpikes)
                spike.Activate();

            isDestroyed = false;
        }

        public void GetDestroyed()
        {
            isDestroyed = true;

            // Desativa os colliders e os sprites
            foreach (var spike in destructibleSpikes)
                spike.Deactivate();

            noteSequence.Deactivate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (!isDestroyed)
                    noteSequence.Activate();
            }
        }
    }
}