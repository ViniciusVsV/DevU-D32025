using Characters.Enemies.SaxPlayer.States;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.SaxPlayer
{
    public class StateController : BaseStateController, IRythmSyncable
    {
        public int moveDirection;

        [Header("||===== Rythm Parameters =====||")]
        [SerializeField] private int beatDelay = 1;
        public int beatCounter;

        [Header("||===== States =====||")]
        [SerializeField] private Idle idleState;
        [SerializeField] private Move moveState;
        [SerializeField] private PlaySax playSaxState;
        [SerializeField] private Die dieState;

        [Header("||===== Booleans =====||")]
        public bool beatHappened;
        public bool isFacingRight;

        protected override void Awake()
        {
            base.Awake();

            idleState.Setup(rb, transform, animator, spriteRenderer, this);
            moveState.Setup(rb, transform, animator, spriteRenderer, this);
            playSaxState.Setup(rb, transform, animator, spriteRenderer, this);
            dieState.Setup(rb, transform, animator, spriteRenderer, this);

            isFacingRight = true;
        }

        private void Start()
        {
            SetIdle(false);
        }

        protected override void Update()
        {
            base.Update();

            beatHappened = false;
        }

        public void SetIdle(bool forced = false) => SetNewState(idleState, forced);
        public void SetMove(bool forced = false) => SetNewState(moveState, forced);
        public void SetPlaySax(bool forced = false) => SetNewState(playSaxState, forced);
        public void SetDie(bool forced = false) => SetNewState(dieState, forced);

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
    }
}