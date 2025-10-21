using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip introMusic;
    public AudioClip normalMusic;

    void Start()
    {
        audioSource.clip = introMusic;
        audioSource.Play();
        Invoke("PlayNormalMusic", Mathf.Min(introMusic.length, 3f));
    }

    void PlayNormalMusic()
    {
        audioSource.clip = normalMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}
