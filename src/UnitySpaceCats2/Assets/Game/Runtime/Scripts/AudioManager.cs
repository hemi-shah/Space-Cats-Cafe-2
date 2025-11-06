using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour, IAudioService
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic;

    public void PlayBackgroundMusic()
    {
        // if the music is already playing then return
        if (audioSource.isPlaying) return;
        
        // if music is not playing, play it
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        audioSource.Stop();
    }
}
