using UnityEngine;

public class Crouch : BaseState
{
    [SerializeField] private AnimationClip animationClip;

    [Header("----- Horizontal Movement -----")]
    [SerializeField] private float moveSpeed;
    private int direction;

    private PlayerController playerController => (PlayerController)controller;

    public override void StateEnter()
    {
        //animator.Play(animationClip.name);
        spriteRenderer.color = Color.magenta;

        isComplete = true;
    }

    public override void StateFixedUpdate()
    {
        if (playerController.moveDirection.x > 0)
            direction = 1;
        else
            direction = playerController.moveDirection.x < 0 ? -1 : 0;

        rb.linearVelocityX = direction * moveSpeed;
    }
}