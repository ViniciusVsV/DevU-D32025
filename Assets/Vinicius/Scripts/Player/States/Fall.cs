using System.Collections;
using UnityEngine;

public class Fall : BaseState
{
    private PlayerController playerController => (PlayerController)controller;

    [SerializeField] private AnimationClip animationClip;

    [Header("||===== Parameters =====||")]
    [SerializeField] private float newGravityScale;
    private float baseGravityScale;

    [Header("||===== Horizontal Movement -----||")]
    [SerializeField] private float moveSpeed;
    private int direction;

    public override void StateEnter()
    {
        //animator.Play(animationClip.name);
        spriteRenderer.color = Color.red;

        baseGravityScale = rb.gravityScale;

        rb.gravityScale = newGravityScale;
    }

    public override void StateUpdate()
    {
        // Transição para Dash
        if (playerController.dashPressed)
            playerController.SetDash();

        // Transição para Knockback
        else if (playerController.tookKnockback)
            playerController.SetKnockback();

        // Transição para Idle
        else if (playerController.isGrounded)
            playerController.SetIdle();
    }

    public override void StateFixedUpdate()
    {
        if (playerController.moveDirection.x > 0)
            direction = 1;
        else
            direction = playerController.moveDirection.x < 0 ? -1 : 0;

        rb.linearVelocityX = direction * moveSpeed;
    }

    public override void StateExit()
    {
        rb.gravityScale = baseGravityScale;
    }
}