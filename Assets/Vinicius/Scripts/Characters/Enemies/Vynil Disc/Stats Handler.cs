using Effects.Complex.Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemies.VynilDisc
{
    public class StatsHandler : MonoBehaviour
    {
        [SerializeField] private StateController vynilDiscController;
        [SerializeField] private SpriteRenderer sprite;

        [SerializeField] private int maxHealth;
        private int currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage()
        {
            currentHealth--;

            DamagedEffects.Instance.ApplyEffects(sprite.transform, sprite);

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