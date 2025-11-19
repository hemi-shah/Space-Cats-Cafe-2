using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioService
{
    public static AudioManager Instance { get; private set; }

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    [Header("SFX")]
    [SerializeField] private AudioClip[] iceSFX;
    [SerializeField] private AudioClip espressoSFX;
    [SerializeField] private AudioClip milkSFX;

    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    private AudioSource MusicSource
    {
        get
        {
            if (_musicSource == null)
            {
                _musicSource = gameObject.AddComponent<AudioSource>();
                _musicSource.loop = true;
                _musicSource.volume = 0.6f;
            }
            return _musicSource;
        }
    }

    private AudioSource SfxSource
    {
        get
        {
            if (_sfxSource == null)
            {
                _sfxSource = gameObject.AddComponent<AudioSource>();
                _sfxSource.volume = 1f; // full volume for SFX
            }
            return _sfxSource;
        }
    }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic == null)
        {
            Debug.LogWarning("No background music assigned");
            return;
        }

        if (!MusicSource.isPlaying)
        {
            MusicSource.clip = backgroundMusic;
            MusicSource.Play();
            Debug.Log("Background music started playing");
        }
    }

    public void PlayEspressoSfx()
    {
        if (espressoSFX == null) return;
        SfxSource.PlayOneShot(espressoSFX);
    }

    public void PlayMilkSfx()
    {
        if (milkSFX == null) return;
        SfxSource.PlayOneShot(milkSFX);
    }

    public void PlayIceSfx()
    {
        if (iceSFX == null || iceSFX.Length == 0) return;
        int index = Random.Range(0, iceSFX.Length);
        SfxSource.PlayOneShot(iceSFX[index]);
    }
    
    public void StopSfx()
    {
        if (_sfxSource != null)
            _sfxSource.Stop();
    }
}
