using Characters.Enemies.SaxPlayer.States;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.SaxPlayer
{
    public class StateController : BaseStateController, IRythmSyncable, IActivatable, IDeactivatable, IRestorable
    {
        [HideInInspector] public int moveDirection;

        [Header("||===== Rythm Parameters =====||")]
        [SerializeField] private int beatDelay;
        public int beatCounter;
        public int playSaxCounter;

        [Header("||===== States =====||")]
        [SerializeField] private Idle idleState;
        [SerializeField] private Move moveState;
        [SerializeField] private PlaySax playSaxState;
        [SerializeField] private Die dieState;
        [SerializeField] private Respawn respawnState;
        [SerializeField] private Deactivate deactivateState;

        [Header("||===== Booleans =====||")]
        public bool beatHappened;
        public bool restored;
        public bool activated;

        public bool isFacingRight;
        public bool isDead;

        protected override void Awake()
        {
            base.Awake();

            idleState.Setup(rb, transform, animator, spriteRenderer, this);
            moveState.Setup(rb, transform, animator, spriteRenderer, this);
            playSaxState.Setup(rb, transform, animator, spriteRenderer, this);
            dieState.Setup(rb, transform, animator, spriteRenderer, this);
            respawnState.Setup(rb, transform, animator, spriteRenderer, this);
            deactivateState.Setup(rb, transform, animator, spriteRenderer, this);

            isFacingRight = true;
        }

        private void Start()
        {
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
        public void SetPlaySax(bool forced = false) => SetNewState(playSaxState, forced);
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

            beatCounter = (beatCounter + 1) % 4; // Mantém o momento da batida no 4:4
        }

        public void Activate() { activated = true; }

        public void Deactivate() { SetDeactivate(); }

        public void Restore() { restored = true; }
    }
}