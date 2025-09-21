using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string PlaySceneName;

    private void Start()
    {
        AudioController.Instance.PlayMainMenuMusic();
    }

    public void Play()
    {
        SceneManager.LoadScene(PlaySceneName);
    }

    public void Exit()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}