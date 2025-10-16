using Characters.Enemies.VynilDisc.States;
using DG.Tweening;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.VynilDisc
{
    public class StateController : BaseStateController, IRythmSyncable, IActivatable, IDeactivatable, IRestorable
    {
        [HideInInspector] public Vector2 followPoint;
        [HideInInspector] public Transform playerTransform;

        [Header("||===== Rythm Parameters =====||")]
        [SerializeField] private Vector3 pulseSize;
        [SerializeField] private Vector3 stunnedPulseSize;
        [SerializeField] private Transform spriteTransform;
        [SerializeField] private int beatDelay;
        private float beatLength;
        public int beatCounter;

        [Header("||===== States =====||")]
        [SerializeField] private Idle idleState;
        [SerializeField] private Move moveState;
        [SerializeField] private Chase chaseState;
        [SerializeField] private WindUp windUpState;
        [SerializeField] private Charge chargeState;
        [SerializeField] private Daze dazeState;
        [SerializeField] private Die dieState;
        [SerializeField] private Respawn respawnState;
        [SerializeField] private Deactivate deactivateState;

        [Header("||===== Booleans =====||")]
        public bool beatHappened;
        public bool restored;
        public bool activated;

        public bool isAggroed;
        public bool isStunned;

        protected override void Awake()
        {
            base.Awake();

            idleState.Setup(rb, transform, animator, spriteRenderer, this);
            moveState.Setup(rb, transform, animator, spriteRenderer, this);
            chaseState.Setup(rb, transform, animator, spriteRenderer, this);
            windUpState.Setup(rb, transform, animator, spriteRenderer, this);
            chargeState.Setup(rb, transform, animator, spriteRenderer, this);
            dazeState.Setup(rb, transform, animator, spriteRenderer, this);
            dieState.Setup(rb, transform, animator, spriteRenderer, this);
            respawnState.Setup(rb, transform, animator, spriteRenderer, this);
            deactivateState.Setup(rb, transform, animator, spriteRenderer, this);
        }

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();

            SetDeactivate(false);
        }

        protected override void Update()
        {
            base.Update();

            beatHappened = false;
            restored = false;
            activated = false;
        }

        public void SetIdle(bool forced = false) => SetNewState(idleState, forced);
        public void SetMove(bool forced = false) => SetNewState(moveState, forced);
        public void SetChase(bool forced = false) => SetNewState(chaseState, forced);
        public void SetWindUp(bool forced = false) => SetNewState(windUpState, forced);
        public void SetCharge(bool forced = false) => SetNewState(chargeState, forced);
        public void SetDaze(bool forced = false) => SetNewState(dazeState, forced);
        public void SetDie(bool forced = false) => SetNewState(dieState, forced);
        public void SetRespawn(bool forced = false) => SetNewState(respawnState, forced);
        public void SetDeactivate(bool forced = false) => SetNewState(deactivateState, forced);

        public void RespondToBeat()
        {
            if (beatDelay > 0)
            {
                beatDelay--;
                return;
            }

            beatHappened = true;

            beatCounter = (beatCounter + 1) % 4;

            Vector3 pulseScale = isStunned ? stunnedPulseSize : pulseSize;

            spriteTransform.localScale = pulseScale;
            spriteTransform.DOScale(Vector3.one, beatLength * 0.9f);
        }

        public void Activate() { activated = true; }

        public void Deactivate() { SetDeactivate(); }

        public void Restore() { restored = true; }
    }
}