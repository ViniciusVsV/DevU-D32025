using System.Collections;
using UnityEngine;

public class Dash : BaseState
{
    [SerializeField] private AnimationClip animationClip;

    [Header("----- Parameters -----")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTimer;
    private int direction;
    private float baseGravityScale;

    private PlayerController playerController => (PlayerController)controller;

    public override void StateEnter()
    {
        //animator.Play(animationClip.name);
        spriteRenderer.color = Color.yellow;

        direction = playerController.isFacingRight ? 1 : -1;

        baseGravityScale = rb.gravityScale;
        rb.gravityScale = 0f;

        rb.linearVelocity = new Vector2(dashSpeed * direction, 0);

        dashTimer = dashDuration;
    }

    public override void StateUpdate()
    {
        if (dashTimer > Mathf.Epsilon)
            dashTimer -= Time.deltaTime;

        else
        {
            rb.gravityScale = baseGravityScale;
            rb.linearVelocity = Vector2.zero;

            if (playerController.isGrounded)
                isComplete = true;
            else
                playerController.Fall();
        }
    }
}