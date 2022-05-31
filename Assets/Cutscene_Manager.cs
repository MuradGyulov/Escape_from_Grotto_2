using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class Cutscene_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] videosLevel_1 = new GameObject[0];
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

        Player_Actions.VideoTrigger.AddListener(VideoNamesReceiver);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            levelNumberIndicator.gameObject.SetActive(false);
            videosLevel_1[0].gameObject.SetActive(true);
        }
    }

    private void Update()
    {

    }

    private void VideoNamesReceiver(string videoTriggerTag)
    {
        skipButton.gameObject.SetActive(true);
        levelNumberIndicator.gameObject.SetActive(false);

        switch(videoTriggerTag)
        {
            case "Video Trigger Level 1.  (2)":
                videosLevel_1[1].gameObject.SetActive(true);
                break;
        }
    }

    public void SkipVideos()
    {
        levelNumberIndicator.gameObject.SetActive(true);

        for(int i = 0; i < videosLevel_1.Length; i++)
        {
            if (videosLevel_1[i].gameObject.activeInHierarchy)
            {
                videosLevel_1[i].gameObject.SetActive(false);
            }
        }

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
