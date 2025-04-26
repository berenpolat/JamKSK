using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public void PlayButton()
    {
        mainMenuPanel.SetActive(false);
        LevelManager.Instance.LoadNextLevel();
    }
    
}
