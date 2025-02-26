using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public enum Languaje
{
    Español,
    English
}
public class Localization_Manager : MonoBehaviour
{
    public static Localization_Manager Instance;

    // web url https://docs.google.com/spreadsheets/d/e/2PACX-1vRtkl-8nGtK1oZhoI6BKGTIBKOZkJWdE9FcJaH7zAJqLE_UHz7MhXzct-mu4KOYM949MjL_GI9h4OO-/pub?output=csv
    [SerializeField] string _WebUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRtkl-8nGtK1oZhoI6BKGTIBKOZkJWdE9FcJaH7zAJqLE_UHz7MhXzct-mu4KOYM949MjL_GI9h4OO-/pub?output=csv";

    [SerializeField] private Languaje _CurrentLang;


    Dictionary<Languaje, Dictionary<string, string>> _LanguajeCodex;

    public event Action OnUpdate = delegate { };

    public void ChangeLanguaje(int LanguajeID)
    {
        var SetNewLanguaje = Languaje.Español;

        switch (LanguajeID)
        {
            case 0:
                SetNewLanguaje = Languaje.Español;
                break;
            case 1:
                SetNewLanguaje = Languaje.English;
                break;
            default:
                SetNewLanguaje = Languaje.Español;
                break;
        }
        _CurrentLang = SetNewLanguaje;
        OnUpdate();
    }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            StartCoroutine(DownloadCodex(_WebUrl));
        }
        else
        {
            Destroy(this);
        }
    }

    IEnumerator DownloadCodex(string URL)
    {
        var wwwCodex = new UnityWebRequest(URL);
        wwwCodex.downloadHandler = new DownloadHandlerBuffer();

        yield return wwwCodex.SendWebRequest();

        if(wwwCodex.result == UnityWebRequest.Result.Success)
        {
            var result = wwwCodex.downloadHandler.text;

            _LanguajeCodex = Localization_Cutter.LoadCodex(result, "WebDownload");

            SaveCodex(FileName:"Codex",Content:result);
        }
        else
        {
            var result = LoadCodex("Codex");
            _LanguajeCodex = Localization_Cutter.LoadCodex(result, "Local");
        }
        OnUpdate();

        //Debug.Log(_LanguajeCodex[Languaje.English]["ID_Config"]);
    }

    public string GetLocalizationText(string ID)
    {
        var resources = _LanguajeCodex[_CurrentLang];
        var result = "ID " + ID + " no existente del lenguaje " + _CurrentLang.ToString();

        resources.TryGetValue(ID, out result);

        return result;
    }

    void SaveCodex(string FileName,string Content)
    {
        string Savepath = Application.persistentDataPath + "/" + FileName;
        try
        {
            System.IO.File.WriteAllText(Savepath, Content);
            Debug.Log("file sucessfully saved at: " + Savepath);
        }
        catch(Exception ex)
        {
            Debug.LogError("Failed to save file: " + ex.ToString());
        }
    }


    string LoadCodex(string FileName)
    {
        string Savepath = Application.persistentDataPath + "/" + FileName;
        try
        {
            if(System.IO.File.Exists(Savepath))
            {
                string content = System.IO.File.ReadAllText(Savepath);
                Debug.Log("File loaded succesfully at: " + Savepath);
                return content;
            }
            else
            {
                Debug.LogError("File not found at: " + Savepath);
                return null;
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("File not found at: " + ex);
            return null;
        }
    }


    public void LaunchUpdateLocalization()
    {
        OnUpdate();
    }
}


