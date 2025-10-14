using UnityEngine;

namespace StateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class BaseStateController : MonoBehaviour
    {
        protected StateMachine stateMachine;

        protected Rigidbody2D rb;
        protected Collider2D col;
        protected Animator animator;
        protected SpriteRenderer spriteRenderer;

        [Header("||===== Health Parameters =====||")]
        [SerializeField] protected int maxHealth;
        protected int currentHealth;

        protected virtual void Awake()
        {
            stateMachine = new StateMachine();

            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            animator = GetComponentInChildren<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            currentHealth = maxHealth;
        }

        protected virtual void Update()
        {
            stateMachine.currentState.StateUpdate();
        }

        protected virtual void FixedUpdate()
        {
            stateMachine.currentState.StateFixedUpdate();
        }

        protected void SetNewState(BaseState newState, bool forced) { stateMachine.SetState(newState, forced); }
        public BaseState GetCurrentState() { return stateMachine.currentState; }

        public virtual void TakeDamage(int damage, Vector2 direction) { }

        protected virtual void Die() { }
    }
}