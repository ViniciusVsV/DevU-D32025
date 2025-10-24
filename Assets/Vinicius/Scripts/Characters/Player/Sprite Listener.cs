using Effects.Complex.Player;
using UnityEngine;

namespace Characters.Player
{
    public class SpriteListener : MonoBehaviour
    {
        private MovementEffects movementEffects;

        private void Start()
        {
            movementEffects = MovementEffects.Instance;
        }

        public void CallRunEffects()
        {
            movementEffects.ApplyRunSFX();
        }
    }
}