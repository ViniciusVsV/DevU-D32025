using DG.Tweening;
using Effects.Complex.Menus;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("||===== Objetcs =====||")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private EventSystem eventSystem;

    private MainMenuEffects mainMenuEffects;
    private SceneExitEffects sceneExitEffects;

    public bool firstFrame = true;

    private void Start()
    {
        if (PlayerPrefs.GetInt("checkpointId") == 0)
        {
            continueButton.enabled = false;
            continueButton.interactable = false;

            eventSystem.firstSelectedGameObject = newGameButton.gameObject;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainMenuEffects = MainMenuEffects.Instance;
        sceneExitEffects = SceneExitEffects.Instance;
    }

    private void Update()
    {
        if (firstFrame)
            mainMenuEffects.ApplyEffects();

        firstFrame = false;
    }

    public void Continue()
    {
        if (PlayerPrefs.GetInt("checkpointId") == 0 || !mainMenuEffects.finishedPlaying)
            return;

        sceneExitEffects.ApplyEffects("Game");
    }

    public void NewGame()
    {
        if (!mainMenuEffects.finishedPlaying)
            return;

        PlayerPrefs.DeleteKey("checkpointId");

        sceneExitEffects.ApplyEffects("Game");
    }

    public void Exit()
    {
        if (!mainMenuEffects.finishedPlaying)
            return;

        Application.Quit();
    }

    public void OpenConfig()
    {

    }
    public void CloseConfig()
    {

    }

    private void OnDestroy() { DOTween.KillAll(); }
}