using UnityEngine;
using YG;

public class Main_Menu_Camera : MonoBehaviour
{
    private Camera menuCamera;

    private void Start()
    {
        menuCamera = GetComponent<Camera>();
    }

    public void ChangeCameraSize( float size)
    {
        menuCamera.orthographicSize = size;
        YandexGame.savesData.cameraSize = size;
        YandexGame.SaveProgress();
    }
}
