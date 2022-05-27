using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject winPanel;
    [Space(10)]
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Text levelNumberIndicator;


    private void Start()
    {
        Player_Actions.PlayerIsWin.AddListener(WinDelay);
        Player_Actions.PlayerIsDead.AddListener(DeadDelay);

        levelNumberIndicator.text = "Level:" + SceneManager.GetActiveScene().buildIndex;
    }

    private void WinDelay() { Invoke("PlayerWin", 2); }
    private void DeadDelay() { Invoke("PlayerDead", 2); }

    public void PauseGame()
    {
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void PlayerWin()
    {
        pauseButton.SetActive(false);
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayerDead()
    {
        pauseButton.SetActive(false);
        deadPanel.SetActive(true);
    }


    public void LoadNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex + 1);
        Time.timeScale = 1;
    }

    public void RepeatLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
