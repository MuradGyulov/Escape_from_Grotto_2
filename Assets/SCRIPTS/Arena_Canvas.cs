using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using YG;

public class Arena_Canvas : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject mobileControlButtonsPanel;
    [Space(18)]
    [SerializeField] private GameObject pauseButton;
    [Space(10)]
    [SerializeField] private Button slugButton;
    [SerializeField] private Button skeletonButton;
    [SerializeField] private Button dragonButton;
    [SerializeField] private Button frogButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button eyeButton;
    [Space(20)]
    [SerializeField] private Transform spawnPointer;
    [Space(15)]
    [SerializeField] private GameObject slugPrefab;
    [SerializeField] private GameObject sckeletonPrefab;
    [SerializeField] private GameObject dragonPrefab;
    [SerializeField] private GameObject frogPrefab;
    [SerializeField] private GameObject hostPrefab;
    [SerializeField] private GameObject eyePrefab;
   
    public static UnityEvent GamePaused = new UnityEvent();

    private void Start()
    {
        Player_Actions.PlayerIsDead.AddListener(DeadDelay);

        Slug_AI.monsterIsDead.AddListener(MonsterIsDead);
        Skeleton_AI.monsterIsDead.AddListener(MonsterIsDead);
        Frog_AI.monssterIsDead.AddListener(MonsterIsDead);
        Dragon_AI.monsterIsDead.AddListener(MonsterIsDead);
        Eye_AI.monsterIsDead.AddListener(MonsterIsDead);

        bool is_Mobile = YandexGame.EnvironmentData.isMobile;
        bool is_Tablet = YandexGame.EnvironmentData.isTablet;
        bool is_Desctop = YandexGame.EnvironmentData.isDesktop;

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
    }

    private void DeadDelay() { Invoke("PlayerDead", 2); }

    public void PauseGame()
    {
        GamePaused.Invoke();
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void PlayerDead()
    {
        deadPanel.SetActive(true);
    }

    public void RepeatLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }

    public void SpawnMnster(string monsterName)
    {
        switch (monsterName)
        {
            case "Slug":
                Instantiate(slugPrefab, spawnPointer.transform.position, transform.rotation);
                slugButton.interactable = false;
                skeletonButton.interactable = false;
                dragonButton.interactable = false;
                frogButton.interactable = false;
                hostButton.interactable = false;
                eyeButton.interactable = false;
                break;
            case "Sckeleton":
                Instantiate(sckeletonPrefab, spawnPointer.transform.position, transform.rotation);
                slugButton.interactable = false;
                skeletonButton.interactable = false;
                dragonButton.interactable = false;
                frogButton.interactable = false;
                hostButton.interactable = false;
                eyeButton.interactable = false;
                break;
            case "Dragon":
                Instantiate(dragonPrefab, spawnPointer.transform.position, transform.rotation);
                slugButton.interactable = false;
                skeletonButton.interactable = false;
                dragonButton.interactable = false;
                frogButton.interactable = false;
                hostButton.interactable = false;
                eyeButton.interactable = false;
                break;
            case "Frog":
                Instantiate(frogPrefab, spawnPointer.transform.position, transform.rotation);
                slugButton.interactable = false;
                skeletonButton.interactable = false;
                dragonButton.interactable = false;
                frogButton.interactable = false;
                hostButton.interactable = false;
                eyeButton.interactable = false;
                break;
            case "Host":
                Instantiate(hostPrefab, spawnPointer.transform.position, transform.rotation);
                slugButton.interactable = false;
                skeletonButton.interactable = false;
                dragonButton.interactable = false;
                frogButton.interactable = false;
                hostButton.interactable = false;
                eyeButton.interactable = false;
                break;
            case "Eye":
                Instantiate(eyePrefab, spawnPointer.transform.position, transform.rotation);
                slugButton.interactable = false;
                skeletonButton.interactable = false;
                dragonButton.interactable = false;
                frogButton.interactable = false;
                hostButton.interactable = false;
                eyeButton.interactable = false;
                break;
        }
    }

    private void MonsterIsDead()
    {
        slugButton.interactable = true;
        skeletonButton.interactable = true;
        dragonButton.interactable = true;
        frogButton.interactable = true;
        hostButton.interactable = true;
        eyeButton.interactable = true;
    }
}
