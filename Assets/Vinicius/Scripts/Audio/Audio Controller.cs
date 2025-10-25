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
    [Header("Player")]
    [SerializeField] private AudioClip[] playerJumpSFXs;
    [SerializeField] private AudioClip[] playerDashSFXs;
    [SerializeField] private AudioClip[] playerWalkSFXs;
    [SerializeField] private AudioClip playerAttackSFX;
    [SerializeField] private AudioClip noteVerde;
    [SerializeField] private AudioClip noteAzul;
    [SerializeField] private AudioClip noteVermelho;
    [SerializeField] private AudioClip noteAmarelo;

    [SerializeField] private AudioClip playerDeathSFX;
    [SerializeField] private AudioClip playerLandSFX;


    [Header("Sax Player")]
    [SerializeField] private AudioClip saxPlayerDeathSFX;
    [SerializeField] private AudioClip saxPlayerAttackSFX;

    [Header("Vynil Disc")]
    [SerializeField] private AudioClip discAggroedSFX;
    [SerializeField] private AudioClip discWindUpSFX;
    [SerializeField] private AudioClip discDashSFX;

    [SerializeField] private AudioClip blockSmashSFX;

    [SerializeField] private AudioClip buttonPressedSFX;

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

    public void PlayPlayerJumpSFX() => PlaySFX(playerJumpSFXs[Random.Range(0, playerJumpSFXs.Length)]);
    public void PlayPlayerDashSFX() => PlaySFX(playerDashSFXs[Random.Range(0, playerDashSFXs.Length)]);
    public void PlayPlayerWalkSFX() => PlaySFX(playerWalkSFXs[Random.Range(0, playerWalkSFXs.Length)]);
    public void PlayPlayerAttackSFX() => PlaySFX(playerAttackSFX);
    public void PlayPlayerDeathSFX() => PlaySFX(playerDeathSFX);
    public void PlayPlayerLandSFX() => PlaySFX(playerLandSFX);

    public void PlaySaxPlayerDeathSFX() => PlaySFX(saxPlayerDeathSFX);
    public void PlaySaxPlayerAttackSFX() => PlaySFX(saxPlayerAttackSFX);

    public void PlayDiscAggroedSFX() => PlaySFX(discAggroedSFX);
    public void PlayDiscDashSFX() => PlaySFX(discDashSFX);
    public void PlayDiscWindUpSFX() => PlaySFX(discWindUpSFX);

    public void PlayBlockSmashSFX() => PlaySFX(blockSmashSFX);

    public void PlayButtonPressedSFX() => PlaySFX(buttonPressedSFX);

    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

    public void PlayNote(MusicalNote note)
    {
        AudioClip clip = note switch
        {
            MusicalNote.Verde => noteVerde,
            MusicalNote.Azul => noteAzul,
            MusicalNote.Vermelho => noteVermelho,
            MusicalNote.Amarelo => noteAmarelo,
            _ => null
        };

        if (clip != null)
            PlaySFX(clip);
    }
}