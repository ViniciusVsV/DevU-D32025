using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemies.VynilDisc
{
    public class StatsHandler : MonoBehaviour
    {
        [SerializeField] private StateController vynilDiscController;

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
                vynilDiscController.SetDie();
        }

        public void Update()
        {
            if (vynilDiscController.restored)
            {
                OnEntityRestored.Invoke();
                currentHealth = maxHealth;
            }
        }
    }
}