using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Player : MonoBehaviour
{
    private static Music_Player musicPlayerInstance;
    private AudioSource audioSource;


    private void Awake()
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
}
