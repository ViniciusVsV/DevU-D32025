using System.Collections;
using UnityEngine;

public class Jump : BaseState
{
    private PlayerController playerController => (PlayerController)controller;

    [SerializeField] private AnimationClip animationClip;

    [Header("||===== Parameters =====||")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCutMultiplier;

    [Header("||===== Horizontal Movement -----||")]
    [SerializeField] private float moveSpeed;
    private int direction;

    public override void StateEnter()
    {
        //animator.Play(animationClip.name);
        spriteRenderer.color = Color.green;

        rb.linearVelocityY = jumpForce;
    }

    public override void StateUpdate()
    {
        // Transição para Jump
        if (playerController.jumpPressed)
            playerController.SetJump(true);

        // Transição para Dash
        else if (playerController.dashPressed)
            playerController.SetDash();

        // Transição para Knockback
        else if (playerController.tookKnockback)
            playerController.SetKnockback();

        // Transição para Fall
        if (rb.linearVelocityY < 0)
            playerController.SetFall();
    }

    public override void StateFixedUpdate()
    {
        if (playerController.moveDirection.x > 0)
            direction = 1;
        else
            direction = playerController.moveDirection.x < 0 ? -1 : 0;

        rb.linearVelocityX = direction * moveSpeed;
    }

    public void JumpCut()
    {
        if (rb.linearVelocityY > 0)
            rb.linearVelocityY *= jumpCutMultiplier;
    }
}