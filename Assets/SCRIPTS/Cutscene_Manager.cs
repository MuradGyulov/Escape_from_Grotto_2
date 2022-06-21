using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class Cutscene_Manager : MonoBehaviour
{
    [SerializeField] private GameObject level_1_Video;
    [SerializeField] private GameObject level_4_Video;
    [SerializeField] private GameObject level_5_Video;
    [SerializeField] private GameObject level_6_Video;
    [SerializeField] private GameObject level_10_Video;
    [SerializeField] private GameObject level_13_Video;
    [SerializeField] private GameObject level_15_Video;
    [SerializeField] private GameObject level_26_Video;
    [SerializeField] private GameObject level_27_Video;
    [SerializeField] private GameObject level_33_Video;
    [SerializeField] private GameObject level_36_Video;
    [SerializeField] private GameObject level_38_Video;
    [SerializeField] private GameObject level_46_Video;
    [SerializeField] private GameObject level_49_Video;
    [SerializeField] private GameObject level_50_Video;
    [Space(26)]
    [SerializeField] private Button skipButton;
    [SerializeField] private GameObject vegnetteHole;
    [SerializeField] private GameObject vegnettePanel;
    [SerializeField] private Text levelNumberIndicator;

    private GameObject Player;
    private Player_Actions playerActions;
    private Player_Input playerInput;
    private Player_Pools playerPools;

    private void Awake()
    {        
        Player = GameObject.FindGameObjectWithTag("Player");
        playerActions = Player.GetComponent<Player_Actions>();
        playerInput = Player.GetComponent<Player_Input>();
        playerPools = Player.GetComponent<Player_Pools>();

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                level_1_Video.SetActive(true);
                break;
            case 4:
                level_4_Video.SetActive(true);
                break;
            case 5:
                level_5_Video.SetActive(true);
                break;
            case 6:
                level_6_Video.SetActive(true);
                break;
            case 10:
                level_10_Video.SetActive(true);
                break;
            case 13:
                level_13_Video.SetActive(true);
                break;
            case 15:
                level_15_Video.SetActive(true);
                break;
            case 26:
                level_26_Video.SetActive(true);
                break;
            case 27:
                level_27_Video.SetActive(true);
                break;
            case 33:
                level_33_Video.SetActive(true);
                break;
            case 36:
                level_36_Video.SetActive(true);
                break;
            case 38:
                level_38_Video.SetActive(true);
                break;
            case 46:
                level_46_Video.SetActive(true);
                break;
            case 49:
                level_49_Video.SetActive(true);
                break;
            case 50:
                level_50_Video.SetActive(true);
                break;
        }

        StartCoroutine(Enabled());
    }

    public void SkipVideos()
    {
        level_1_Video.SetActive(false);
        level_4_Video.SetActive(false);
        level_5_Video.SetActive(false);
        level_6_Video.SetActive(false);
        level_10_Video.SetActive(false);
        level_13_Video.SetActive(false);
        level_15_Video.SetActive(false);
        level_26_Video.SetActive(false);
        level_27_Video.SetActive(false);
        level_33_Video.SetActive(false);
        level_36_Video.SetActive(false);
        level_38_Video.SetActive(false);
        level_46_Video.SetActive(false);
        level_49_Video.SetActive(false);
        level_50_Video.SetActive(false);


        vegnetteHole.SetActive(false);
        vegnettePanel.SetActive(false);
        skipButton.gameObject.SetActive(false);
        levelNumberIndicator.gameObject.SetActive(true);

        playerActions.enabled = true;
        playerInput.enabled = true;
        playerPools.enabled = true; 
        StopCoroutine(Enabled());
    }


    private IEnumerator Enabled()
    {
        while (true)
        {
            if (playerActions.enabled == true)
            {
                levelNumberIndicator.gameObject.SetActive(true);
                skipButton.gameObject.SetActive(false);

                StopEnabled();
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void StopEnabled()
    {
        StopCoroutine(Enabled());
    }
}
