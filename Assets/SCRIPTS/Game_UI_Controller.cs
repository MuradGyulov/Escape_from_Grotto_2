using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject winPanel;
    [Space(10)]
    [SerializeField] private GameObject pauseButton;

    private int sceneIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) { Delay(); }
        if (Input.GetKeyDown(KeyCode.P)) { PlayerWin(); }
    }

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }



    private void Delay() { Invoke("PlayerDead", 2f); }

    public void PauseGame()
    {
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach(var audio in audioSources)
        {
            audio.Stop();
        }
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
        SceneManager.LoadScene(sceneIndex + 1);
        Time.timeScale = 1;
    }

    public void RepeatLevel()
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
