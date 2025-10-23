using System;
using DG.Tweening;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.VynilDisc.States
{
    public class Move : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Objects =====||")]
        [SerializeField] private Transform spriteTransform;

        [Header("||===== Parameters =====||")]
        private float beatLength;

        private Tween tween;

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();
        }

        public override void StateEnter()
        {
            animator.Play(animationClip.name, 0, 0);

            //Flipa o sprite
            if (Mathf.Sign((vynilDiscController.followPoint - rb.position).x) != Mathf.Sign(spriteTransform.localScale.x))
            {
                Vector3 newScale = spriteTransform.localScale;
                newScale.x *= -1;
                spriteTransform.localScale = newScale;
            }

            tween = rb.DOMove(vynilDiscController.followPoint, beatLength).SetEase(Ease.OutCirc);
        }

        public override void StateUpdate()
        {
            // Transição para Chase
            if (vynilDiscController.isAggroed)
            {
                tween?.Kill();

                vynilDiscController.SetChase();
            }

            // Transição para Idle
            else if (vynilDiscController.beatHappened)
            {
                tween?.Kill();

                vynilDiscController.SetIdle();
            }
        }
    }
}