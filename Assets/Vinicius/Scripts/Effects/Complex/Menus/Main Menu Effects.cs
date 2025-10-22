using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Effects.Simple;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace Effects.Complex.Menus
{
    public class MainMenuEffects : MonoBehaviour, IRythmSyncable
    {
        public static MainMenuEffects Instance;

        [Header("||===== Objects =====||")]
        [Header("Transition")]
        [SerializeField] private Image devULogo;
        private RectTransform devULogoTr;
        [SerializeField] private RectTransform blackScreen;

        [Header("Buttons")]
        [SerializeField] private List<RectTransform> buttons = new();
        private List<Vector2> buttonsStartPositions = new();

        [SerializeField] private float buttonsInitialDisplacement;

        [Header("Sliders")]
        [SerializeField] private List<RectTransform> sliders = new();
        private List<Vector2> slidersStartPositions = new();

        [SerializeField] private float slidersInitialDisplacement;

        [Header("Other")]
        [SerializeField] private RectTransform gameLogo;
        [SerializeField] private AudioSource musicSource;

        private CameraShake cameraShake;
        private ControllerRumble controllerRumble;
        private MusicMuffling musicMuffling;
        private LocalizedShockwave localizedShockwave;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float mainDelay;
        [SerializeField] private float fadeInDelay;
        [SerializeField] private float buttonsDelay;
        [SerializeField] private float gamelogoDelay;

        [SerializeField] private float devULogoSizePulse;
        private Color logoColor;

        [SerializeField] private int beatsDuration;

        [SerializeField] private float transitionDuration;
        [SerializeField] private Ease transitionEase;

        [SerializeField] private float musicDemufflingDuration;

        [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;
        [SerializeField] private float cameraShakeStrength;

        [SerializeField] private float buttonMovementDuration;
        [SerializeField] private float nextButtonDelay;
        [SerializeField] private Ease buttonEase;

        [SerializeField] private float gameLogoDuration;
        [SerializeField] private float gameLogoInitialSize;
        [SerializeField] private float gameLogoCameraShakeStrength;
        [SerializeField] private Ease gameLogoEase;

        [SerializeField] private float shockwaveSize;
        [SerializeField] private float shockwaveDuration;

        [Header("Controller Rumble Parameters")]
        [SerializeField] private float lowFrequency;
        [SerializeField] private float highFrequency;
        [SerializeField] private float rumbleDuration;

        private float screenWidth;
        private Vector2 offScreenLeft;

        private float beatLength;
        private float totalLength;

        private int beatCounter = -1;

        [Header("||===== Booleans =====||")]
        public bool finishedPlaying;

        private void Awake()
        {
            Instance = this;

            devULogoTr = devULogo.GetComponent<RectTransform>();
            gameLogo.localScale = Vector3.zero;

            logoColor = Color.white;
            logoColor.a = 0;
            devULogo.color = logoColor;

            foreach (var button in buttons)
            {
                buttonsStartPositions.Add(button.position);
                button.position += new Vector3(buttonsInitialDisplacement, 0, 0);
            }
            foreach (var slider in sliders)
            {
                slidersStartPositions.Add(slider.position);
                slider.position += new Vector3(0, -slidersInitialDisplacement, 0);
            }

            screenWidth = blackScreen.rect.width;
            offScreenLeft = new Vector2(-screenWidth, 0);
        }

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();
            totalLength = beatLength * beatsDuration;

            cameraShake = FindFirstObjectByType<CameraShake>();
            controllerRumble = FindFirstObjectByType<ControllerRumble>();
            musicMuffling = FindFirstObjectByType<MusicMuffling>();
            localizedShockwave = FindFirstObjectByType<LocalizedShockwave>();
        }

        private void OnEnable() { BeatInterval.OnOneBeatHappened += RespondToBeat; }
        private void OnDisable() { BeatInterval.OnOneBeatHappened -= RespondToBeat; }

        public void ApplyEffects()
        {
            finishedPlaying = false;

            AudioController.Instance.PlayMainMenuMusic();
            musicSource.Pause();

            musicMuffling.ApplyEffect(0);

            StartCoroutine(MainRoutine());
            StartCoroutine(FadeInRoutine());
        }

        private IEnumerator MainRoutine()
        {
            yield return new WaitForSeconds(mainDelay);

            musicSource.Play();
            musicMuffling.RemoveEffect(totalLength);

            while (beatCounter == -1)
                yield return null;

            beatCounter++;

            while (beatCounter < beatsDuration)
                yield return null;

            blackScreen.DOAnchorPos(offScreenLeft, transitionDuration).SetEase(transitionEase)
                .OnComplete(() =>
                {
                    cameraShake.ApplyEffect(cinemachineImpulseSource, cameraShakeStrength, Vector2.zero);
                    controllerRumble.ApplyEffect(lowFrequency, highFrequency, rumbleDuration, Vector2.zero);

                    StartCoroutine(ButtonsRoutine());
                });
        }

        private IEnumerator FadeInRoutine()
        {
            yield return new WaitForSeconds(fadeInDelay);

            float elapsedTime = 0;
            float progress;

            while (elapsedTime < totalLength - fadeInDelay)
            {
                progress = elapsedTime / (totalLength - fadeInDelay);

                logoColor.a = Mathf.Lerp(0, 1, progress);
                devULogo.color = logoColor;

                elapsedTime += Time.deltaTime;

                yield return null;
            }
        }

        private IEnumerator ButtonsRoutine()
        {
            yield return new WaitForSeconds(buttonsDelay);

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].DOMove(buttonsStartPositions[i], buttonMovementDuration).SetEase(buttonEase);

                if (i < buttons.Count - 1)
                    yield return new WaitForSeconds(nextButtonDelay);
            }

            for (int i = 0; i < sliders.Count; i++)
            {
                sliders[i].DOMove(slidersStartPositions[i], buttonMovementDuration).SetEase(buttonEase);

                if (i < sliders.Count - 1)
                    yield return new WaitForSeconds(nextButtonDelay);
            }

            yield return new WaitForSeconds(gamelogoDelay);

            gameLogo.localScale = gameLogoInitialSize * Vector3.one;

            gameLogo.DOScale(Vector3.one, gameLogoDuration).SetEase(gameLogoEase)
                .OnComplete(() =>
                {
                    cameraShake.ApplyEffect(cinemachineImpulseSource, gameLogoCameraShakeStrength, Vector2.zero);
                    controllerRumble.ApplyEffect(lowFrequency, highFrequency, rumbleDuration, Vector2.zero);
                    localizedShockwave.ApplyEffect(shockwaveDuration, gameLogo.position, shockwaveSize * Vector3.one);

                    finishedPlaying = true;
                });
        }

        public void RespondToBeat()
        {
            beatCounter++;

            if (beatCounter < 5)
            {
                devULogoTr.DOKill();

                devULogoTr.localScale = devULogoSizePulse * Vector3.one;

                devULogoTr.DOScale(Vector3.one, beatLength);
            }
        }
    }
}