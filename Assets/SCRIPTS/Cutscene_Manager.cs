using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class Cutscene_Manager : MonoBehaviour
{
    [SerializeField] private GameObject level_1_Video;
    [SerializeField] private GameObject level_2_Video;
    [SerializeField] private GameObject Level_4_Video;
    [SerializeField] private GameObject Level_7_Video;
    [Space(30)]
    [SerializeField] private Button skipButton;
    [SerializeField] private Text levelNumberIndicator;
    [SerializeField] private GameObject vegnetteHole;
    [SerializeField] private GameObject vegnettePanel;

    GameObject player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(Closer());


        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                levelNumberIndicator.gameObject.SetActive(false);
                level_1_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 2:
                levelNumberIndicator.gameObject.SetActive(false);
                level_2_Video.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 4:
                levelNumberIndicator.gameObject.SetActive(false);
                Level_4_Video.gameObject.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
            case 7:
                levelNumberIndicator.gameObject.SetActive(false);
                Level_7_Video.gameObject.SetActive(true);
                skipButton.gameObject.SetActive(true);
                break;
        }
    }

    public void SkipVideos()
    {
        levelNumberIndicator.gameObject.SetActive(true);

        level_1_Video.SetActive(false);
        level_2_Video.SetActive(false);
        Level_4_Video.SetActive(false);
        Level_7_Video.SetActive(false);

        skipButton.gameObject.SetActive(false);
        vegnetteHole.SetActive(false);
        vegnettePanel.SetActive(false);

        player.GetComponent<Player_Input>().enabled = true;
        player.GetComponent<Player_Actions>().enabled = true;
        player.GetComponent<Player_Pools>().enabled = true;
    }

    private IEnumerator Closer()
    {
        while (true)
        {

            yield return new WaitForSeconds(2);

            if (player.GetComponent<Player_Input>().enabled == true)
            {
                levelNumberIndicator.gameObject.SetActive(true);
                skipButton.gameObject.SetActive(false);
            }
            else if(player.GetComponent<Player_Input>().enabled == false)
            {
                levelNumberIndicator.gameObject.SetActive(false);
                skipButton.gameObject.SetActive(true);
            }
        }
    }
}
