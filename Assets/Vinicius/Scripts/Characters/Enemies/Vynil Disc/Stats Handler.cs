using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemies.VynilDisc
{
    public class StatsHandler : MonoBehaviour
    {
        [SerializeField] private StateController vynilDiscController;

        [SerializeField] private int maxHealth;
        private int currentHealth;

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
                currentHealth = maxHealth;
        }
    }
}