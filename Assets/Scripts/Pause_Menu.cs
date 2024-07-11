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
        AudioManager.instance?.StopMasterMusicAudio("ID_Pause");
        PauseMenu.SetActive(false);
        Pause = false;
        AudioManager.instance?.PlayMasterMusicAudio("ID_Radar");
        Time.timeScale = 1;
        
    }

    public void MainMenu(string MenuName)
    {
        SceneManager.LoadScene(MenuName);
        AudioManager.instance?.StopMasterMusicAudio("ID_Pause");
    }
    public void Quit( )
    {
        Application.Quit();
    }

}
