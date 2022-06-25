using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject levelsPanel;
    [Space(25)]
    [SerializeField] Button[] levelButtons = new Button[50];

    private int commpletedLevels;

    private void Start()
    {
        commpletedLevels = PlayerPrefs.GetInt("Completed levels");
        
        for(int i = 0; i < commpletedLevels; i++)
        {
            levelButtons[i].interactable = true;
        }
        levelButtons[0].interactable = true;
    }

    public void OpenSettingsPanel() { settingsPanel.SetActive(true); }
    public void CloseSettingsPanel() { settingsPanel.SetActive(false); }
    public void OpenLevelsPanel() { levelsPanel.SetActive(true); }
    public void CloselevelsPanel() { levelsPanel.SetActive(false); }
    public void LoadLevel(int levelIndex) { SceneManager.LoadScene(levelIndex); }
}
