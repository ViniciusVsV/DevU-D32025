using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("||===== Music =====||")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip gameMusic;

    [Header("||===== SFX =====||")]
    [SerializeField] private AudioClip clickSFX;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayMainMenuMusic()
    {
        if (musicSource.clip != mainMenuMusic)
        {
            musicSource.clip = mainMenuMusic;
            musicSource.Play();
        }
    }
    public void PlayGameMusic()
    {
        if (musicSource.clip != gameMusic)
        {
            musicSource.clip = gameMusic;
            musicSource.Play();
        }
    }

    public void PlayClickSFX() => PlaySFX(clickSFX);
    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }
}