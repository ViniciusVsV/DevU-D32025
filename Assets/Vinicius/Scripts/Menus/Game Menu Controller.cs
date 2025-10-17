using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetCheckpoint()
    {
        PlayerPrefs.DeleteKey("checkpointId");
    }
}