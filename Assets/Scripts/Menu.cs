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

        AsyncLoadManager._Instance.LoadAsyncLevel("Campaign Layer");
        
    }
     public void Campaign()
    {
        AsyncLoadManager._Instance.LoadAsyncLevel("Campaign Layer");

    }

    public void Click()
    {
        AudioManager.instance?.PlayMasterSfxAudio("ID_Click");

    }

    public void Salir()
    {
        AudioManager.instance?.PlayMasterSfxAudio("ID_Abort");
        AudioManager.instance?.PlayMasterSfxAudio("ID_MotorTurnOff");
        StartCoroutine(WaitAndQuit());
      
        
    }

    private IEnumerator WaitAndQuit()
    {
        yield return new WaitForSeconds(1f);
        Application.Quit();
        
    }
}