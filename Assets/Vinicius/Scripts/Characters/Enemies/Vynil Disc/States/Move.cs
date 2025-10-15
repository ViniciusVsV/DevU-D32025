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

        [Header("||===== Parameters =====||")]
        [Range(0, 1)][SerializeField] private float beatLengthPercentage; //O quanto do tempo da batida que será usado para o movimento
        private float duration;

        private Tween tween;

        private void Start()
        {
            duration = BeatController.Instance.GetBeatLength();
        }

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.cyan;


            //Flipa o sprite
            if (Mathf.Sign((vynilDiscController.followPoint - rb.position).x) != Mathf.Sign(tr.localScale.x))
            {
                Vector3 newScale = tr.localScale;
                newScale.x *= -1;
                tr.localScale = newScale;
            }

            //Movimento dura uma batida
            tween = rb.DOMove(vynilDiscController.followPoint, duration * beatLengthPercentage).SetEase(Ease.OutCirc)
                .OnComplete(() => vynilDiscController.SetIdle());
        }

        public override void StateUpdate()
        {
            if (vynilDiscController.isAggroed)
            {
                tween?.Kill();

                vynilDiscController.SetChase();
            }
        }
    }
}