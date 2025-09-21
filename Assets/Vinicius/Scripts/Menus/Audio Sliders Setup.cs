using UnityEngine;
using UnityEngine.UI;

public class AudioSlidersSetup : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
            masterSlider.value = PlayerPrefs.GetFloat("masterVolume");

        if (PlayerPrefs.HasKey("musicVolume"))
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        if (PlayerPrefs.HasKey("sfxVolume"))
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
}