using DG.Tweening;
using Effects.Complex.Scenes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("||===== Objetcs =====||")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private EventSystem eventSystem;

    private MenuEnterEffects menuEnterEffects;
    private MenuExitEffects menuExitEffects;

    public bool firstFrame = true;

    private void Start()
    {
        if (PlayerPrefs.GetInt("checkpointId") == 0)
        {
            continueButton.enabled = false;
            continueButton.interactable = false;

            eventSystem.firstSelectedGameObject = newGameButton.gameObject;
        }

        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;

        menuEnterEffects = MenuEnterEffects.Instance;
        menuExitEffects = MenuExitEffects.Instance;
    }

    private void Update()
    {
        if (firstFrame)
            menuEnterEffects.ApplyEffects();

        firstFrame = false;
    }

    public void Continue()
    {
        if (PlayerPrefs.GetInt("checkpointId") == 0 || !menuEnterEffects.finishedPlaying)
            return;

        continueButton.interactable = false;
        newGameButton.interactable = false;
        exitButton.interactable = false;

        menuExitEffects.ApplyEffects("Game");
    }

    public void NewGame()
    {
        if (!menuEnterEffects.finishedPlaying)
            return;

        continueButton.interactable = false;
        newGameButton.interactable = false;
        exitButton.interactable = false;

        PlayerPrefs.DeleteKey("checkpointId");

        menuExitEffects.ApplyEffects("Game");
    }

    public void Exit()
    {
        if (!menuEnterEffects.finishedPlaying)
            return;

        continueButton.interactable = false;
        newGameButton.interactable = false;
        exitButton.interactable = false;

        Application.Quit();
    }

    private void OnDestroy() { DOTween.KillAll(); }
}