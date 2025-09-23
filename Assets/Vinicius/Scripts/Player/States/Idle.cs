using UnityEngine;

public class Idle : BaseState
{
    [SerializeField] private AnimationClip animationClip;

    public override void StateEnter()
    {
        rb.linearVelocityX = 0;
        
        //animator.Play(animationClip.name);
        spriteRenderer.color = Color.blue;

        isComplete = true;
    }
}