using Characters.Enemies;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossController : MonoBehaviour, IActivatable, IDeactivatable, IRestorable
{
    [Header("-----States-----")]
    [SerializeField] public BossIdleState idleState;
    [SerializeField] public BossAttackState attackState;
    [SerializeField] public BassSolo bassSolo;
    [SerializeField] public KeyboardSolo keyboardSolo;
    [SerializeField] public DrumSolo drumSolo;
    [SerializeField] public NoteSequence noteSequence;
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
    public void Die()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        bossStateMachine.Update();
    }

    public void Activate()
    {
        noteSequence.Activate();
    }

    public void Deactivate()
    {
        noteSequence.Deactivate();
    }

    public void Restore()
    {
        noteSequence.Activate();
    }
}
