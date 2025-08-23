using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;  
    [SerializeField] AudioSource SFXSource;

    [Header("Tracks")]
    public AudioClip[] tracks;

    [Header("Sound Effects")]
    public AudioClip jump;
    public AudioClip doubleJump;
    public AudioClip takeHit;
    public AudioClip die;

    public LevelManager levelManager;

    void Start()
    {
        //musicSource.clip = tracks[Random.Range(0, tracks.Length)];
        //musicSource.Play();
    }

    private void Update()
    {
        if (levelManager.isPlayerMoving && !musicSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayRandomMusic()
    {
        musicSource.clip = tracks[Random.Range(0, tracks.Length)];
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
