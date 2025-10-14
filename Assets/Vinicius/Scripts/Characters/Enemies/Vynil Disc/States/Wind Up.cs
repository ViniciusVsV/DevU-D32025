using DG.Tweening;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.VynilDisc.States
{
    public class WindUp : BaseState
    {
        private StateController houndController => (StateController)controller;

        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Parameters =====||")]
        [Range(0, 1)][SerializeField] private float beatLengthPercentage;
        private float beatLength;

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();
        }

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.blue;

            tr.DORotate(new Vector3(0, 0, 360), beatLength * beatLengthPercentage, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(Ease.OutExpo);
        }

        public override void StateUpdate()
        {
            // Transição para Charge
            if (houndController.beatHappened)
                houndController.SetCharge();
        }
    }
}