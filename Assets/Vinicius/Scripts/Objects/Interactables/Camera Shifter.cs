using Unity.Cinemachine;
using UnityEngine;

namespace Objects.Interactables
{
    public class CameraShifter : MonoBehaviour
    {
        [Header("||==== Objects ====||")]
        [SerializeField] private CinemachineCamera newCamera;

        private CinemachineCamera baseCamera;
        private Rigidbody2D playerRb;

        [Header("||==== Parameters ====||")]
        [SerializeField] private float timeToActivate;
        private float timer;

        private void Start()
        {
            baseCamera = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineCamera>();
        }

        private void Update()
        {
            if (playerRb != null)
            {
                if (playerRb.linearVelocity.sqrMagnitude < 0.01f)
                    timer += Time.deltaTime;

                else
                {
                    timer = 0;

                    baseCamera.Priority = 10;
                    newCamera.Priority = 0;
                }

                if (timer >= timeToActivate)
                {
                    baseCamera.Priority = 0;
                    newCamera.Priority = 10;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                playerRb = other.GetComponent<Rigidbody2D>();
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                timer = 0;

                playerRb = null;

                baseCamera.Priority = 10;
                newCamera.Priority = 0;
            }
        }

    }
}