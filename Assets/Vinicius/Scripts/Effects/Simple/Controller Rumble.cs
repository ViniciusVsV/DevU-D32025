using System.Collections;
using Characters.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Effects.Simple
{
    public class ControllerRumble : MonoBehaviour
    {
        private Gamepad gamepad;

        private InputHandler playerInput;

        private Coroutine coroutine;

        private void Start()
        {
            playerInput = FindFirstObjectByType<InputHandler>();
        }

        public void ApplyEffect(float lowFrequency, float highFrequency, float duration)
        {
            if (!playerInput.isOnController)
                return;

            gamepad = Gamepad.current;

            if (gamepad != null)
            {
                if (coroutine != null)
                    StopCoroutine(coroutine);

                coroutine = StartCoroutine(Routine(lowFrequency, highFrequency, duration));
            }
        }

        private IEnumerator Routine(float lowFrequency, float highFrequency, float duration)
        {
            gamepad.SetMotorSpeeds(lowFrequency, highFrequency);

            yield return new WaitForSeconds(duration);

            gamepad.SetMotorSpeeds(0f, 0f);

            coroutine = null;
        }
    }
}