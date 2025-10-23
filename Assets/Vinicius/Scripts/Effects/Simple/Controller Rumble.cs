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

        [SerializeField] private float maxDistance;
        [SerializeField] private AnimationCurve dampingCurve;
        private float dampingFactor;

        private Coroutine coroutine;

        public bool isPlaying;

        private void Start()
        {
            playerInput = FindFirstObjectByType<InputHandler>();
        }

        public void ApplyEffect(float lowFrequency, float highFrequency, float duration, Vector2 position)
        {
            if (!playerInput.isOnController)
                return;

            gamepad = Gamepad.current;

            if (gamepad != null)
            {
                if (position != Vector2.zero)
                {
                    float distance = Vector2.Distance(position, playerInput.transform.position);

                    if (distance > maxDistance)
                        return;

                    dampingFactor = dampingCurve.Evaluate(distance / maxDistance);

                    lowFrequency *= dampingFactor;
                    highFrequency *= dampingFactor;
                }

                isPlaying = true;

                if (coroutine != null)
                    StopCoroutine(coroutine);

                coroutine = StartCoroutine(Routine(lowFrequency, highFrequency, duration));
            }
        }

        private IEnumerator Routine(float lowFrequency, float highFrequency, float duration)
        {
            gamepad.SetMotorSpeeds(lowFrequency, highFrequency);

            yield return new WaitForSecondsRealtime(duration);

            gamepad.SetMotorSpeeds(0f, 0f);

            isPlaying = false;

            coroutine = null;
        }

        private void OnDisable()
        {
            gamepad = Gamepad.current;
            gamepad?.SetMotorSpeeds(0f, 0f);
        }
        private void OnDestroy()
        {
            gamepad = Gamepad.current;
            gamepad?.SetMotorSpeeds(0f, 0f);
        }
    }
}