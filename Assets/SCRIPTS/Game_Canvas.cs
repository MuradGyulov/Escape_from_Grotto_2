using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class Game_Canvas : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject winPanel;
    [Space(18)]
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject skipButton;
    [Space(18)]
    [SerializeField] private Text levelNumberIndicator;

    public static UnityEvent GamePaused = new UnityEvent();
    public static UnityEvent ContinueGame = new UnityEvent();

    private void Start()
    {
        if(Application.systemLanguage == SystemLanguage.Russian)
        {
            levelNumberIndicator.text = "Уровень:" + SceneManager.GetActiveScene().buildIndex;
        }
        else
        {
            levelNumberIndicator.text = "Level:" + SceneManager.GetActiveScene().buildIndex;
        }

        Player_Actions.PlayerIsWin.AddListener(WinDelay);
        Player_Actions.PlayerIsDead.AddListener(DeadDelay);
    }

    private void WinDelay() { Invoke("PlayerWin", 2); }
    private void DeadDelay() { Invoke("PlayerDead", 2); }

    public void PauseGame()
    {
        GamePaused.Invoke();
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinuePlaying()
    {
        ContinueGame.Invoke();
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void PlayerWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayerDead()
    {
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
