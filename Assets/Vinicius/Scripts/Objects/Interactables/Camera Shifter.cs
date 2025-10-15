using Unity.Cinemachine;
using UnityEngine;

namespace Objects.Interactables
{
    public class CameraShifter : MonoBehaviour
    {
        [Header("||==== Objects ====||")]
        [SerializeField] private CinemachineCamera cam; //Serializable para caso queira ver o gizmos da câmera no editor
        private CinemachineFollow cinemachineFollow;
        [SerializeField] private Transform focusPoint;

        [Header("||==== Parameters ====||")]
        [SerializeField] private float timeToActivate;
        private float timer;

        private bool isColliding;
        public bool showGizmos; //Habilta mostrar o gizmos

        private void Start()
        {
            cam = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineCamera>();
            cinemachineFollow = cam.gameObject.GetComponent<CinemachineFollow>();
        }

        private void Update()
        {
            timer = isColliding ? timer + Time.deltaTime : 0;

            if (timer >= timeToActivate)
                cam.Follow = focusPoint;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                isColliding = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isColliding = false;

                cam.Follow = other.transform;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (cam != null)
                cinemachineFollow = cam.GetComponent<CinemachineFollow>();
        }
#endif

        private void OnDrawGizmos()
        {
            if (!showGizmos || cam == null)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, focusPoint.position);

            var lens = cam.Lens;
            if (lens.Orthographic)
            {
                float height = lens.OrthographicSize * 2f;
                float width = height * lens.Aspect;

                Vector3 camPos = focusPoint.position;

                camPos += cinemachineFollow.FollowOffset;

                Vector3 topLeft = camPos + new Vector3(-width / 2f, height / 2f, 0f);
                Vector3 topRight = camPos + new Vector3(width / 2f, height / 2f, 0f);
                Vector3 bottomLeft = camPos + new Vector3(-width / 2f, -height / 2f, 0f);
                Vector3 bottomRight = camPos + new Vector3(width / 2f, -height / 2f, 0f);

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(topLeft, topRight);
                Gizmos.DrawLine(topRight, bottomRight);
                Gizmos.DrawLine(bottomRight, bottomLeft);
                Gizmos.DrawLine(bottomLeft, topLeft);
            }
        }
    }
}