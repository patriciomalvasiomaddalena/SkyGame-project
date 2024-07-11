using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

   

    [SerializeField] TextMeshProUGUI EnergyUI;

    private void Start()
    {
        AudioManager.instance?.PlayMasterMusicAudio("ID_Menu");
    }



    public void Jugar()
    {
       
        SceneManager.LoadScene(1);
        
    }
    public void Tutorial()
    {
        SceneManager.LoadScene(2);

    }
     public void Campaign()
    {
        SceneManager.LoadScene(1);

    }

     public void Orb()
    {
        SceneManager.LoadScene(4);

    }
    
    public void Credits()
    {
        SceneManager.LoadScene(4);
     
    } public void Click()
    {
        AudioManager.instance?.PlayMasterSfxAudio("ID_Click");

    }

    public void Salir()
    {
        AudioManager.instance?.PlayMasterSfxAudio("ID_Abort");
        StartCoroutine(WaitAndQuit());
      
        
    }

    private IEnumerator WaitAndQuit()
    {
        yield return new WaitForSeconds(1f);
        Application.Quit();
        
    }
}