using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using YG;

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
    [SerializeField] private GameObject level_54_Video;
    [SerializeField] private GameObject level_55_Video;
    [Space(26)]
    [SerializeField] private Text levelNumberIndicator;
    [SerializeField] private Button skipButton;
    [SerializeField] private GameObject vegnetteHole;
    [SerializeField] private GameObject vegnettePanel;
    [SerializeField] private GameObject mobileControlButtonsPanel;


    private GameObject Player;
    private Player_Actions playerActions;
    private Player_Input playerInput;
    private Player_Pools playerPools;

    private bool is_Tablet;
    private bool is_Mobile;
    private bool is_Desctop;

    private void Start()
    {
        is_Tablet = YandexGame.EnvironmentData.isTablet;
        is_Mobile = YandexGame.EnvironmentData.isMobile;

        Player = GameObject.FindGameObjectWithTag("Player");
        playerActions = Player.GetComponent<Player_Actions>();
        playerInput = Player.GetComponent<Player_Input>();
        playerPools = Player.GetComponent<Player_Pools>();

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                level_1_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 4:
                level_4_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 5:
                level_5_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 6:
                level_6_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 10:
                level_10_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 13:
                level_13_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 15:
                level_15_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 26:
                level_26_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 27:
                level_27_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 33:
                level_33_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 36:
                level_36_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 38:
                level_38_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 46:
                level_46_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 54:
                level_54_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 55:
                level_55_Video.SetActive(true);
                break;
            default:
                if (is_Mobile)
                {
                    mobileControlButtonsPanel.SetActive(true);
                }
                else if (is_Tablet)
                {
                    mobileControlButtonsPanel.SetActive(true);
                }
                else if (is_Desctop)
                {
                    mobileControlButtonsPanel.SetActive(false);
                }
                else
                {
                    mobileControlButtonsPanel.SetActive(false);
                }
                break;
        }

        StartCoroutine(EnableCutscenePlayer());
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
        level_54_Video.SetActive(false);
        level_55_Video.SetActive(false);

        vegnetteHole.SetActive(false);
        vegnettePanel.SetActive(false);
        skipButton.gameObject.SetActive(false);

        levelNumberIndicator.gameObject.SetActive(true);
        if (is_Mobile)
        {
            mobileControlButtonsPanel.SetActive(true);
        }
        else if (is_Tablet)
        {
            mobileControlButtonsPanel.SetActive(true);
        }
        else if (is_Desctop)
        {
            mobileControlButtonsPanel.SetActive(false);
        }
        else
        {
            mobileControlButtonsPanel.SetActive(false);
        }

        playerActions.enabled = true;
        playerInput.enabled = true;
        playerPools.enabled = true; 
        StopCoroutine(EnableCutscenePlayer());
    }


    private IEnumerator EnableCutscenePlayer()
    {
        while (true)
        {
            if (playerActions.enabled == true)
            {
                levelNumberIndicator.gameObject.SetActive(true);
                skipButton.gameObject.SetActive(false);
                if (is_Mobile)
                {
                    mobileControlButtonsPanel.SetActive(true);
                }
                else if (is_Tablet)
                {
                    mobileControlButtonsPanel.SetActive(true);
                }
                else if (is_Desctop)
                {
                    mobileControlButtonsPanel.SetActive(false);
                }
                else
                {
                    mobileControlButtonsPanel.SetActive(false);
                }

                StopEnabled();
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void StopEnabled()
    {
        StopCoroutine(EnableCutscenePlayer());
    }
}
