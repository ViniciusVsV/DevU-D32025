using DG.Tweening;
using Effects.Complex.Menus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("||===== Objetcs =====||")]
    [SerializeField] private Button continueButton;

    private MainMenuEffects mainMenuEffects;

    public bool firstFrame = true;

    private void Start()
    {
        if (PlayerPrefs.GetInt("checkpointId") == 0)
            continueButton.interactable = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainMenuEffects = MainMenuEffects.Instance;
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

        SceneManager.LoadScene("Game");
    }

    public void NewGame()
    {
        if (!mainMenuEffects.finishedPlaying)
            return;

        PlayerPrefs.DeleteKey("checkpointId");
        SceneManager.LoadScene("Game");
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