using UnityEngine;
using UnityEngine.InputSystem;
using Objects.Platforms;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformInteractor : MonoBehaviour
    {
        private Rigidbody2D rb;

        public OneWayPlatform currentOneWayPlatform;
        [SerializeField] private float doubleCrouchThreshold;
        private float doubleCrouchTimer;

        [SerializeField] private float ledgePanTime;
        private float ledgePanTimer;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (doubleCrouchTimer > Mathf.Epsilon)
                doubleCrouchTimer -= Time.deltaTime;
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
            {
                transform.parent = collision.transform;
                rb.interpolation = RigidbodyInterpolation2D.None;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("OneWayPlatform"))
                currentOneWayPlatform = null;

            else if (collision.gameObject.CompareTag("MovingPlatform"))
            {
                transform.parent = null;
                rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
    }
}