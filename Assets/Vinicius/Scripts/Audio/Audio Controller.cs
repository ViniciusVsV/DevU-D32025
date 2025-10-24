using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("||===== Music =====||")]
    [SerializeField] private AudioClip mainMenuStartMusic;
    [SerializeField] private AudioClip mainMenuReturnMusic;
    [SerializeField] private AudioClip gameMusic;

    [Header("||===== SFX =====||")]
    [SerializeField] private AudioClip discAggroedSFX;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayMainMenuStartMusic()
    {
        if (musicSource.clip != mainMenuStartMusic)
        {
            musicSource.clip = mainMenuStartMusic;
            musicSource.Play();
        }
    }

    public void PlayMainMenuReturnMusic()
    {
        if (musicSource.clip != mainMenuReturnMusic)
        {
            musicSource.clip = mainMenuReturnMusic;
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

    public void PlayDiscAggroedSFX() => PlaySFX(discAggroedSFX);
    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }
}