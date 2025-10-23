using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Effects.Simple
{
    public class CameraZoom : MonoBehaviour
    {
        private CinemachineCamera mainCam;

        private float currentZoom;

        private Coroutine coroutine;

        private void Start()
        {
            mainCam = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineCamera>();
        }

        public void ApplyEffect(float newZoom, float duration)
        {
            currentZoom = mainCam.Lens.OrthographicSize;

            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(Routine(newZoom, duration));
        }

        private IEnumerator Routine(float newZoom, float duration)
        {
            float elapsedTime = 0;

            float progress;

            while (elapsedTime < duration)
            {
                progress = elapsedTime / duration;

                mainCam.Lens.OrthographicSize = Mathf.Lerp(currentZoom, newZoom, progress);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            mainCam.Lens.OrthographicSize = newZoom;

            coroutine = null;
        }
    }
}