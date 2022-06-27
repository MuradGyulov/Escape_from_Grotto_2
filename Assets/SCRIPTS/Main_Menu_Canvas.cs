using System.Collections;
using System.Collections.Generic;
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

    private GameObject musicPlayer;
    private Music_Player musicPlayerScript;
    private AudioSource musicPlayerAudiosourceComponen;

    private GameObject mainMenuCamera;
    private Main_Menu_Camera mainMenuCameraScript;
    private Camera mainMenuCameraComponent;


    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;


    private void Start()
    {
        musicPlayer = GameObject.FindGameObjectWithTag("Music Player");
        musicPlayerScript = musicPlayer.GetComponent<Music_Player>();
        musicPlayerAudiosourceComponen = musicPlayer.GetComponent<AudioSource>();

        mainMenuCamera = GameObject.FindGameObjectWithTag("Main Menu Camera");
        mainMenuCameraScript = mainMenuCamera.GetComponent<Main_Menu_Camera>();
        mainMenuCameraComponent = mainMenuCamera.GetComponent<Camera>();

        GetLoad();  
    }

    public void ResetPlayerData()
    {
  
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i != 0) { levelButtons[i].interactable = false; }
            YandexGame.ResetSaveProgress();
        }
        
    }

    public void GetLoad()
    {
        soundsSlider.value = YandexGame.savesData.soundsVolume;

        musicSlider.value = YandexGame.savesData.musicVolume;
        musicPlayerAudiosourceComponen.volume = YandexGame.savesData.musicVolume;

        cameraSlider.value = YandexGame.savesData.cameraSize;
        mainMenuCameraComponent.orthographicSize = cameraSlider.value;

        int savesCompletedLevels = YandexGame.savesData.completedLevels;
        for (int i = 0; i < savesCompletedLevels; i++)
        {
            levelButtons[i].interactable = true;
        }
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
    public void CloselevelsPanel() { levelsPanel.SetActive(false); }
    public void LoadLevel(int levelIndex) { SceneManager.LoadScene(levelIndex); }





    public void OpenSettingsPanel() { settingsPanel.SetActive(true); }
    public void CloseSettingsPanel()  { settingsPanel.SetActive(false); YandexGame.SaveProgress();}
        
    public void ChangeSoundsVolumeSlider()
    {
        YandexGame.savesData.soundsVolume = soundsSlider.value;
    }

    public void ChangeMusicVolumeSlider()
    {
        musicPlayerScript.ChangingMusicVolume(musicSlider.value);
        YandexGame.savesData.musicVolume = musicSlider.value;
    }

    public void ChangeCameraSizeSlider()
    {
        mainMenuCameraScript.ChangeCameraSize(cameraSlider.value);
        YandexGame.savesData.cameraSize = cameraSlider.value;
    }
}
