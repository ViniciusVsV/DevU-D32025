using Characters.Enemies.Hound.States;
using DG.Tweening;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.Hound
{
    public class StateController : BaseStateController, IRythmSyncable
    {
        [HideInInspector] public Vector2 followPoint;
        [HideInInspector] public Transform playerTransform;

        [Header("||===== Rythm Parameters =====||")]
        [SerializeField] private Vector3 pulseSize;
        [SerializeField] private Vector3 stunnedPulseSize;
        [SerializeField] private Transform spriteTransform;
        [SerializeField] private int beatDelay;
        private float beatLength;

        [Header("||===== States =====||")]
        [SerializeField] private Idle idleState;
        [SerializeField] private Move moveState;
        [SerializeField] private Chase chaseState;
        [SerializeField] private WindUp windUpState;
        [SerializeField] private Charge chargeState;
        [SerializeField] private Daze dazeState;

        [Header("||===== Booleans =====||")]
        public bool isAggroed;
        public bool isStunned;
        public bool beatHappened;

        protected override void Awake()
        {
            base.Awake();

            idleState.Setup(rb, transform, animator, spriteRenderer, this);
            moveState.Setup(rb, transform, animator, spriteRenderer, this);
            chaseState.Setup(rb, transform, animator, spriteRenderer, this);
            windUpState.Setup(rb, transform, animator, spriteRenderer, this);
            chargeState.Setup(rb, transform, animator, spriteRenderer, this);
            dazeState.Setup(rb, transform, animator, spriteRenderer, this);

            SetIdle(false);
        }

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();
        }

        protected override void Update()
        {
            base.Update();

            beatHappened = false;
        }

        public void SetIdle(bool forced = false) => SetNewState(idleState, forced);
        public void SetMove(bool forced = false) => SetNewState(moveState, forced);
        public void SetChase(bool forced = false) => SetNewState(chaseState, forced);
        public void SetWindUp(bool forced = false) => SetNewState(windUpState, forced);
        public void SetCharge(bool forced = false) => SetNewState(chargeState, forced);
        public void SetDaze(bool forced = false) => SetNewState(dazeState, forced);

        public void RespondToBeat()
        {
            if (beatDelay > 0)
            {
                beatDelay--;
                return;
            }

            beatHappened = true;

            Vector3 pulseScale = isStunned ? stunnedPulseSize : pulseSize;

            spriteTransform.localScale = pulseScale;
            spriteTransform.DOScale(Vector3.one, beatLength * 0.9f);
        }
    }
}