using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause_Menu : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool Pause = false;

    private void Update()
    {
    }
    public void PauseAction()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Pause = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        
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
