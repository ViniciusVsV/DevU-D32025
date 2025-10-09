using DG.Tweening;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.VynilDisc.States
{
    public class Daze : BaseState
    {
        private StateController houndController => (StateController)controller;

        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float initialSpeed;
        [SerializeField] private float deceleration;
        [SerializeField] private int beatsDuration;
        private int beatCounter;

        private float beatLength;

        private Vector2 direction;
        private Vector2 speedDiff;

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();
        }

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.white;

            houndController.isStunned = true;

            beatCounter = 0;

            direction = (houndController.followPoint - rb.position).normalized * -1;
        }

        public override void StateUpdate()
        {
            if (houndController.beatHappened)
            {
                if (beatCounter == 0)
                    rb.linearVelocity = initialSpeed * direction;

                tr.DORotate(new Vector3(0, 0, 360), beatLength, RotateMode.FastBeyond360)
                    .SetRelative(true);

                beatCounter++;

                // Transição para Chase
                if (beatCounter > beatsDuration)
                    houndController.SetChase();
            }
        }

        public override void StateFixedUpdate()
        {
            speedDiff = Vector2.zero - rb.linearVelocity;

            rb.AddForce(speedDiff * deceleration, ForceMode2D.Force);
        }

        public override void StateExit()
        {
            houndController.isStunned = false;

            rb.linearVelocity = Vector2.zero;
        }
    }
}