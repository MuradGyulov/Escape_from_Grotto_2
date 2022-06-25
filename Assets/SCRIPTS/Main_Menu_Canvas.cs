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
    [SerializeField] Button[] levelButtons = new Button[50];

    private int currentCompletedLevels;

    private void Start()
    {
        currentCompletedLevels = YandexGame.savesData.completedLevel;
        print(currentCompletedLevels);

        for(int i = 0; i < currentCompletedLevels; i++)
        {
            levelButtons[i].interactable = true;
        }
    }

    public void OpenSettingsPanel() { settingsPanel.SetActive(true); }
    public void CloseSettingsPanel() { settingsPanel.SetActive(false); }
    public void OpenLevelsPanel() { levelsPanel.SetActive(true); }
    public void CloselevelsPanel() { levelsPanel.SetActive(false); }
    public void LoadLevel(int levelIndex) { SceneManager.LoadScene(levelIndex); }
}
