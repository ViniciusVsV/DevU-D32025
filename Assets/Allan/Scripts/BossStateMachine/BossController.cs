using Characters.Enemies.Beamer.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossController : MonoBehaviour
{
    [Header("-----States-----")]
    [SerializeField] public BossIdleState idleState;
    [SerializeField] public BossAttackState attackState;
    [SerializeField] public BassSolo bassSolo;
    [SerializeField] public KeyboardSolo keyboardSolo;
    [SerializeField] public DrumSolo drumSolo;
    private BossStateMachine bossStateMachine;

    [Header("-----Health------")]
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [Header("-----Attack Timer-----")]
    [SerializeField] public float attackInterval;

    void Awake()
    {
        bossStateMachine = new BossStateMachine();
        idleState = new BossIdleState(this, bossStateMachine);
        attackState = new BossAttackState(this, bossStateMachine);

        currentHealth = maxHealth;
    }

    void Start()
    {
        bossStateMachine.ChangeState(idleState);
    }

    void Update()
    {
        bossStateMachine.Update();
    }
}
