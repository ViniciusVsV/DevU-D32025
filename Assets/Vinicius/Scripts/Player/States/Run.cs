using UnityEngine;

public class Run : BaseState
{
    [SerializeField] private AnimationClip animationClip;

    [Header("----- Parameters -----")]
    [SerializeField] private float moveSpeed;
    private int direction;

    private PlayerController playerController => (PlayerController)controller;

    public override void StateEnter()
    {
        //animator.Play(animationClip.name);
        spriteRenderer.color = Color.cyan;

        isComplete = true;
    }

    public override void StateFixedUpdate()
    {
        direction = playerController.moveDirection.x > 0 ? 1 : -1;

        rb.linearVelocityX = direction * moveSpeed;
    }
}