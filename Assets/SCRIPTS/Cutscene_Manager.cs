using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class Cutscene_Manager : MonoBehaviour
{
    [SerializeField] private GameObject level_1_Video;
    [SerializeField] private Button skipButton;
    [SerializeField] private GameObject vegnetteHole;
    [SerializeField] private GameObject vegnettePanel;
    [SerializeField] private Text levelNumberIndicator;


    private void Awake()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                level_1_Video.SetActive(true);
                break;
        }
    }

    public void SkipVideos()
    {
        level_1_Video.SetActive(false);

        vegnetteHole.SetActive(false);
        vegnettePanel.SetActive(false);
        skipButton.gameObject.SetActive(false);
        levelNumberIndicator.gameObject.SetActive(true);
    }
}
