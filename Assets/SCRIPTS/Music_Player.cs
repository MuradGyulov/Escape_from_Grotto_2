
using UnityEngine;

public class Music_Player : MonoBehaviour
{
    private static Music_Player musicPlayerInstance;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
        
        if(musicPlayerInstance == null)
        {
            musicPlayerInstance = this;
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }

    public void ChangingMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
