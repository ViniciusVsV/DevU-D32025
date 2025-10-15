using StateMachine;
using UnityEngine;

namespace Characters.Enemies.SaxPlayer.States
{
    public class Die : BaseState
    {
        [SerializeField] private GameObject saxPlayer;

        public override void StateEnter()
        {
            Destroy(saxPlayer);
        }
    }
}