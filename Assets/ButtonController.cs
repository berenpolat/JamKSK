using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject levelFailPanel;
    public GameObject completePanel;
    public void PlayButton()
    {
        mainMenuPanel.SetActive(false);
    }

    public void PlayLevelAgainButton()
    {
        levelFailPanel.SetActive(false);
        LevelManager.Instance.RestartLevel();
    }

    public void NextLevelButton()
    {
        levelFailPanel.SetActive(false);
        LevelManager.Instance.LoadNextLevel();
    }
    public void BackToMenuButton()
    {
        completePanel.SetActive(false);
        LevelManager.Instance.LoadNextLevel();
    }
}
