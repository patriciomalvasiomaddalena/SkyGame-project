using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

    AudioSource AU;

    [SerializeField] TextMeshProUGUI EnergyUI;



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

    }





    public void Salir()
    {
        Application.Quit();
    }
}