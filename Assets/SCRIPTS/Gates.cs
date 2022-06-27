using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Gates : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            YandexGame.savesData.completedLevels = sceneIndex + 1;
            YandexGame.SaveProgress();
        }
    }
}
