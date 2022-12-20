using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Main_Menu_Canvas : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject levelsPanel;
    [Space(25)]
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider cameraSlider;
    [Space(25)]
    [SerializeField] Button[] levelButtons = new Button[50];

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private Camera mainMenuCameraComponent;
    private AudioSource musicPlayerAudiosourceComponent;

    private GameObject musicPlayer;
    private GameObject mainoMenuCamera;



    private void Start()
    {
        GetLoad();
    }

    

    public void GetLoad()
    {
        soundsSlider.value = YandexGame.savesData.soundsVolume;

        musicPlayer = GameObject.FindGameObjectWithTag("Music Player");
        musicPlayerAudiosourceComponent = musicPlayer.GetComponent<AudioSource>();
        musicPlayerAudiosourceComponent.volume = YandexGame.savesData.musicVolume;
        musicSlider.value = YandexGame.savesData.musicVolume;

        mainoMenuCamera = GameObject.FindGameObjectWithTag("Main Menu Camera");
        mainMenuCameraComponent = mainoMenuCamera.GetComponent<Camera>();
        mainMenuCameraComponent.orthographicSize = YandexGame.savesData.cameraSize;
        cameraSlider.value = YandexGame.savesData.cameraSize;
    }

    public void ChangeSoundsVolume()
    {
        YandexGame.savesData.soundsVolume = soundsSlider.value;
    }

    public void ChangeMusicVolume()
    {
        musicPlayerAudiosourceComponent.volume = musicSlider.value;
        YandexGame.savesData.musicVolume = musicSlider.value;
    }

    public void ChangeCameraSize()
    {
        mainMenuCameraComponent.orthographicSize = cameraSlider.value;
        YandexGame.savesData.cameraSize = cameraSlider.value;
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex); 
    }

    public void OpenSettingsPanel() 
    {
        settingsPanel.SetActive(true); 
    }

    public void CloseSettingsPanel()  
    {
        settingsPanel.SetActive(false);
        YandexGame.SaveProgress();
    }

    public void CloselevelsPanel() 
    {
        levelsPanel.SetActive(false); 
    }

    public void OpenLevelsPanel() 
    {
        levelsPanel.SetActive(true);
        int savesCompletedLevels = YandexGame.savesData.completedLevels;
        for (int i = 0; i < savesCompletedLevels; i++)
        {
            levelButtons[i].interactable = true;
        }
    }
}
