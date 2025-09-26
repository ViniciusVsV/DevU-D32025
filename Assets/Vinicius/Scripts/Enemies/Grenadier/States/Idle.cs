using StateMachine;
using UnityEngine;

namespace Enemies.Grenadier.States
{
    public class Idle : BaseState
    {
        [SerializeField] private AnimationClip animationClip;

        [SerializeField] private float flipDuration;
        private float flipTimer;
        [SerializeField] private float maxFlipVariation;

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);

            flipTimer = Random.Range(flipDuration - maxFlipVariation, flipDuration + maxFlipVariation);
        }

        public override void StateUpdate()
        {
            if (flipTimer > Mathf.Epsilon)
                flipTimer -= Time.deltaTime;
            else
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

                flipTimer = Random.Range(flipDuration - maxFlipVariation, flipDuration + maxFlipVariation);
            }
        }
    }
}