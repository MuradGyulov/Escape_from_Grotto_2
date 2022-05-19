using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject levelsPanel;

    public void OpenSettingsPanel() { settingsPanel.SetActive(true); }
    public void CloseSettingsPanel() { settingsPanel.SetActive(false); }
    public void OpenLevelsPanel() { levelsPanel.SetActive(true); }
    public void CloselevelsPanel() { levelsPanel.SetActive(false); }
    public void LoadLevel(int levelIndex) { SceneManager.LoadScene(levelIndex); }
}
