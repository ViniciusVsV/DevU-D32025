using Characters.Player;
using Effects.Simple.LightningBolt;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    [SerializeField] private StateController stateController;
    [SerializeField] private LightningBoltManager lightningBoltManager;

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

        else if (Input.GetKeyDown(KeyCode.P))
            stateController.SetDie();

        else if (Input.GetKeyDown(KeyCode.L))
            lightningBoltManager.SummonBolt(stateController.transform.position, Vector2.zero);
    }
}