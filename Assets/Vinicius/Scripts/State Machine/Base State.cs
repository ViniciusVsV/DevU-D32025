using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected Rigidbody2D rb;
    protected Transform tr;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected BaseController controller;

    public void Setup(Rigidbody2D rb, Transform tr, Animator animator, SpriteRenderer spriteRenderer, BaseController controller)
    {
        this.rb = rb;
        this.tr = tr;
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.controller = controller;
    }

    public void Initialise()
    {
        isComplete = false;
    }

    public virtual void StateEnter() { }
    public virtual void StateUpdate() { }
    public virtual void StateFixedUpdate() { }
    public virtual void StateExit() { }
}