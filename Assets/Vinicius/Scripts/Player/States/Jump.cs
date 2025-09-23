using System.Collections;
using UnityEngine;

public class Jump : BaseState
{
    [SerializeField] private AnimationClip animationClip;

    [Header("||===== Parameters =====||")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCutMultiplier;

    [Header("||===== Horizontal Movement -----||")]
    [SerializeField] private float moveSpeed;
    private int direction;

    private PlayerController playerController => (PlayerController)controller;

    public override void StateEnter()
    {
        //animator.Play(animationClip.name);
        spriteRenderer.color = Color.green;

        rb.linearVelocityY = jumpForce;
    }

    public override void StateUpdate()
    {
        if (rb.linearVelocityY < 0)
        {
            isComplete = true;

            playerController.Fall();
        }
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