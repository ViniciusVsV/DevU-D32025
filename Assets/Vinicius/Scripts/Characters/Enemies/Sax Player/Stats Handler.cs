using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemies.SaxPlayer
{
    public class StatsHandler : MonoBehaviour, IDamageable
    {
        [SerializeField] private StateController saxPlayerController;

        [SerializeField] private int maxHealth;
        private int currentHealth;

        public UnityEvent OnEntityRestored;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage()
        {
            currentHealth--;

            if (currentHealth <= 0)
                saxPlayerController.SetDie();
        }

        public void Update()
        {
            if (saxPlayerController.restored)
            {
                OnEntityRestored.Invoke();
                currentHealth = maxHealth;
            }
        }
    }
}