using UnityEngine;
using UnityEngine.InputSystem;
using Objects.Platforms;

namespace Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformInteractor : MonoBehaviour
    {
        private StateController playerController;

        private OneWayPlatform currentOneWayPlatform;
        private Rigidbody2D movingPlatformRb;

        [SerializeField] private float doubleCrouchThreshold;
        private float doubleCrouchTimer;

        private void Awake()
        {
            playerController = GetComponent<StateController>();
        }

        private void Update()
        {
            if (doubleCrouchTimer > Mathf.Epsilon)
                doubleCrouchTimer -= Time.deltaTime;

            if (movingPlatformRb != null)
                playerController.platformVelocity = movingPlatformRb.linearVelocity;
            else
                playerController.platformVelocity = Vector2.zero;
        }

        public void CrouchInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (doubleCrouchTimer > Mathf.Epsilon && currentOneWayPlatform != null)
                    currentOneWayPlatform.DisableCollision();
                else
                    doubleCrouchTimer = doubleCrouchThreshold;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("OneWayPlatform"))
                currentOneWayPlatform = collision.gameObject.GetComponent<OneWayPlatform>();

            else if (collision.gameObject.CompareTag("MovingPlatform"))
                movingPlatformRb = collision.gameObject.GetComponent<Rigidbody2D>();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("OneWayPlatform"))
                currentOneWayPlatform = null;

            else if (collision.gameObject.CompareTag("MovingPlatform"))
                movingPlatformRb = null;
        }
    }
}