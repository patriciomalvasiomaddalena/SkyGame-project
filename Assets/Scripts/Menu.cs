using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

    AudioSource AU;

    [SerializeField] TextMeshProUGUI EnergyUI;

    private void Start()
    {
        EnergyUI.text = "" + GameManager.Instance.Energy;
    }

    private void Update()
    {
        EnergyUI.text = "energia" + GameManager.Instance.Energy;
    }

    public void Jugar()
    {
        SceneManager.LoadScene(0);

    }
    public void Tutorial()
    {
        SceneManager.LoadScene(2);

    }
     public void Campaign()
    {
        SceneManager.LoadScene(3);

    }

     public void Orb()
    {
        SceneManager.LoadScene(4);

    }



    public void Salir()
    {
        Application.Quit();
    }
}