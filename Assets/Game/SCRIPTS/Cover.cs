using UnityEngine;
using YG;

public class Cover : MonoBehaviour
{
    private void Start()
    {
        if (YandexGame.EnvironmentData.isDesktop)
        {
            gameObject.SetActive(false);
        }
    }
}
