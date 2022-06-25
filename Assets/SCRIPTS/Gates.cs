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
            YandexGame.savesData.completedLevel = SceneManager.GetActiveScene().buildIndex + 1;
            YandexGame.SaveProgress();
        }
    }
}
