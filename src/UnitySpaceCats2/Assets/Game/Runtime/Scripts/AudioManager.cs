using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioService
{
    public static AudioManager Instance { get; private set; }

    [Header("Background Music")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic;

    [Header("SFX")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] iceSFX;
    [SerializeField] private AudioClip espressoSFX;
    [SerializeField] private AudioClip milkSFX;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
        }

        if (!sfxSource)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (!backgroundMusic)
        {
            Debug.LogWarning("No background music assigned");
            return;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.clip = backgroundMusic;
            audioSource.Play();
            Debug.Log("Background music started playing");
        }
    }
    
    public void PlayEspressoSfx()
    {
        if (!espressoSFX) return;
        sfxSource.PlayOneShot(espressoSFX);
        Debug.Log("Espresso SFX played");
    }

    public void PlayMilkSfx()
    {
        if (!milkSFX) return;
        sfxSource.PlayOneShot(milkSFX);
        Debug.Log("Milk SFX played");
    }

    public void PlayIceSfx()
    {
        if (iceSFX == null || iceSFX.Length == 0) return;

        // pick a random ice clip
        int index = Random.Range(0, iceSFX.Length);
        sfxSource.PlayOneShot(iceSFX[index]);
        Debug.Log("Ice SFX played");
    }
}