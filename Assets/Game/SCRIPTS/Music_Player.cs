using UnityEngine;

public class Music_Player : MonoBehaviour
{
    private static Music_Player musicPlayerInstance;

    private void Start()
    {
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
