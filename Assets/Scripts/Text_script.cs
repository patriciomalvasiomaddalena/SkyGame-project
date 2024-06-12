using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class StartDialogo : MonoBehaviour
{
    // Text refferences
    public string[] dialogueLines;
    public TMP_Text tmpScreen;
    public TMP_FontAsset textFont;
    public float writingSpeed = 0.05f;
    private int lineaActual = 0;

   

    // Button references
    public GameObject nextButton;
    public GameObject skipButton;

    void Start()
    {
        if (tmpScreen == null)
        {
            Debug.LogError("No se ha asignado ningún componente de texto en pantalla. Se requiere un TMP_Text.");
            return;
        }

        if (textFont == null)
        {
            Debug.LogError("No se ha asignado ninguna fuente de texto personalizada. Se requiere una TMP_FontAsset.");
            return;
        }

        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogError("No hay líneas de diálogo definidas en el array.");
            return;
        }

        tmpScreen.font = textFont;

        StarText();
    }

    void StarText()
    {
       
        Writing(lineaActual);
    }

    void Writing(int indiceLinea)
    {
        if (indiceLinea < dialogueLines.Length)
        {
            StartCoroutine(MostrarDialogoConRetardo(dialogueLines[indiceLinea]));
        }
        else
        {
            Debug.LogWarning("No hay más líneas de diálogo disponibles.");
        }
    }

    IEnumerator MostrarDialogoConRetardo(string textoLinea)
    {
        for (int i = 0; i < textoLinea.Length; i++)
        {
            tmpScreen.text += textoLinea[i];
            yield return new WaitForSecondsRealtime(writingSpeed);
        }
    }

    public void OnNextButtonClick()
    {
        lineaActual++;

        tmpScreen.text = "";
        Writing(lineaActual);

        nextButton.SetActive(lineaActual < dialogueLines.Length - 1);
        skipButton.SetActive(lineaActual < dialogueLines.Length);
    }

    public void OnSkipButtonClick()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    void Update()
    {
        nextButton.SetActive(lineaActual < dialogueLines.Length - 1);
    }
}
