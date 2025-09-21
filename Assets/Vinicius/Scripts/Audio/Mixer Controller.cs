using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
            audioMixer.SetFloat("masterVolume", Mathf.Log10(PlayerPrefs.GetFloat("masterVolume")) * 20);

        if (PlayerPrefs.HasKey("musicVolume"))
            audioMixer.SetFloat("musicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);

        if (PlayerPrefs.HasKey("sfxVolume"))
            audioMixer.SetFloat("sfxVolume", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume")) * 20);
    }

    public void SetMasterVolume(float newVolume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(newVolume) * 20);
        PlayerPrefs.SetFloat("masterVolume", newVolume);
    }

    public void SetMusicVolume(float newVolume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(newVolume) * 20);
        PlayerPrefs.SetFloat("musicVolume", newVolume);
    }

    public void SetSFXVolume(float newVolume)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(newVolume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", newVolume);
    }
}