using UnityEngine;

public class Run : BaseState
{
    private PlayerController playerController => (PlayerController)controller;

    [SerializeField] private AnimationClip animationClip;

    [Header("||===== Parameters =====||")]
    [SerializeField] private float moveSpeed;
    private int direction;

    public override void StateEnter()
    {
        //animator.Play(animationClip.name);
        spriteRenderer.color = Color.cyan;
    }

    public override void StateUpdate()
    {
        // Transição para Jump
        if (playerController.jumpPressed)
            playerController.SetJump();

        // Transição para Dash
        else if (playerController.dashPressed)
            playerController.SetDash();

        // Transição para Knockback
        else if (playerController.tookKnockback)
            playerController.SetKnockback();

        // Transição para Idle
        else if (Mathf.Abs(playerController.moveDirection.x) <= 0.01f)
            playerController.SetIdle();

        // Transição para Crouch
        else if (playerController.isCrouching)
            playerController.SetCrouch();

        // Transição para Fall
        else if (rb.linearVelocityY < 0f)
            playerController.SetFall();
    }

    public override void StateFixedUpdate()
    {
        direction = playerController.moveDirection.x > 0 ? 1 : -1;

        rb.linearVelocityX = direction * moveSpeed;
    }
}