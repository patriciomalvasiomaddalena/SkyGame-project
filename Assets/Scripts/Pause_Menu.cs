using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause_Menu : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool Pause = false;

    public void PauseAction()
    {
        Time.timeScale = 0;
        AudioManager.instance?.PlayMasterMusicAudio ("ID_Pause");

    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Pause = false;
        Time.timeScale = 1;
        
        
    }

    public void MainMenu(string MenuName)
    {
        SceneManager.LoadScene(MenuName);
    }
    public void Quit( )
    {
        Application.Quit();
    }

}
