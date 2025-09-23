using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class BaseController : MonoBehaviour
{
    protected StateMachine stateMachine;

    protected Rigidbody2D rb;
    protected Collider2D col;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    public bool isBusy;

    protected virtual void Awake()
    {
        stateMachine = new StateMachine();

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        stateMachine.currentState.StateUpdate();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.currentState.StateFixedUpdate();
    }
}