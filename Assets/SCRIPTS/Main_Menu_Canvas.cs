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

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;


    private GameObject mainoMenuCamera;
    private Camera mainMenuCameraComponent;

    private GameObject musicPlayer;
    private AudioSource musicPlayerAudiosourceComponent;


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





    public void ResetPlayerData()
    {

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i != 0) { levelButtons[i].interactable = false; }
            YandexGame.savesData.completedLevels = 0;
            YandexGame.SaveProgress();
        }

    }

    public void SetNumberOfcompletedLevels(int numberOfcompletedlevels)
    {
        for (int i = 0; i < numberOfcompletedlevels; i++)
        {
            levelButtons[i].interactable = true;
            YandexGame.savesData.completedLevels = numberOfcompletedlevels;
            YandexGame.SaveProgress();
        }
    }
}
