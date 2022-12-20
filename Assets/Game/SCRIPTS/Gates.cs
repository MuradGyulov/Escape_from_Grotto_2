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
            if(sceneIndex >= YandexGame.savesData.completedLevels)
            {
                YandexGame.savesData.completedLevels = sceneIndex + 1;
                YandexGame.SaveProgress();
            }
            else if (sceneIndex == 54)
            {
                YandexGame.savesData.completedLevels = 55;
                YandexGame.SaveProgress();
            }
        }
    }
}
