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
        EnergyUI.text = "energia" + GameManager.Instance.Energy;
    }

    private void Update()
    {
        EnergyUI.text = "energia" + GameManager.Instance.Energy;
    }

    public void Jugar()
    {
        SceneManager.LoadScene(0);

    }

    public void Salir()
    {
        Application.Quit();
    }
}