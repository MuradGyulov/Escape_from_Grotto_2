using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Main_Menu_Camera : MonoBehaviour
{
    private Camera menuCamera;

    private void Start()
    {
        menuCamera = GetComponent<Camera>();
    }

    public void ChangeCameraSize(float size)
    {
        menuCamera.orthographicSize = size;
    }
}
