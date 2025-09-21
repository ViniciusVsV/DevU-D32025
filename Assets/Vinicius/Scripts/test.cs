using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    private void Start()
    {
        AudioController.Instance.PlayGameMenuMusic();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            AudioController.Instance.PlayClickSFX();

        else if (Input.GetMouseButtonDown(1))
            SceneManager.LoadScene("Main Menu");
    }
}