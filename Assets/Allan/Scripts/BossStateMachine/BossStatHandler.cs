using Characters.Enemies;
using Effects.Complex.Enemies;
using UnityEngine;
using UnityEngine.Events;



public class BossStatHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private BossController bossController;
    [SerializeField] private NoteSequence noteSequence;

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
            bossController.Die();
    }

    public void Update()
    {

    }
}
