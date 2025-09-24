using UnityEngine;

public class WallJump : BaseState
{
    PlayerController playerController => (PlayerController)controller;

    [SerializeField] private AnimationClip animationClip;

    [SerializeField] private Vector2 baseWallJumpForce;
    private Vector2 currentWallJumpForce;

    public override void StateEnter()
    {
        //animator.Play(animationClip.name);
        spriteRenderer.color = Color.magenta;

        currentWallJumpForce = baseWallJumpForce;
        currentWallJumpForce.x *= playerController.isFacingRight ? -1 : 1;

        rb.linearVelocity = currentWallJumpForce;
    }

    public override void StateUpdate()
    {
        // Transição para Dash
        if (playerController.dashPressed)
            playerController.SetDash();

        // Transição pra Knockback
        else if (playerController.tookKnockback)
            playerController.SetKnockback();

        // Transição para Fall
        else if (rb.linearVelocityY < 0f)
            playerController.SetFall();
    }
}