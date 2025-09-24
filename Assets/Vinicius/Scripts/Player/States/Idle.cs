using UnityEngine;

public class Idle : BaseState
{
    private PlayerController playerController => (PlayerController)controller;

    [SerializeField] private AnimationClip animationClip;

    public override void StateEnter()
    {
        rb.linearVelocityX = 0;

        //animator.Play(animationClip.name);
        spriteRenderer.color = Color.blue;
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

        // Transição para Run
        else if (Mathf.Abs(playerController.moveDirection.x) > 0.01f)
            playerController.SetRun();

        // Transição para Crouch
        else if (playerController.isCrouching)
            playerController.SetCrouch();

        // Transição para Fall
        else if (rb.linearVelocityY < 0f)
            playerController.SetFall();
    }
}