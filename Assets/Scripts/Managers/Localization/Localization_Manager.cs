using System;
using System.Collections;
using System.Collections.Generic;
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

    public void ChangeLanguaje(Languaje Newlanguaje)
    {
        _CurrentLang = Newlanguaje;
        OnUpdate();
    }


    private void Awake()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(DownloadCodex(_WebUrl));

        if(Instance == null)
        {
            Instance = this;
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

        var result = wwwCodex.downloadHandler.text;

        _LanguajeCodex = Localization_Cutter.LoadCodex(result,"WebDownload");

        //Debug.Log(_LanguajeCodex[Languaje.English]["ID_Config"]);
    }

    public string GetLocalizationText(string ID)
    {
        var resources = _LanguajeCodex[_CurrentLang];
        var result = "ID no existente del lenguaje " + _CurrentLang.ToString();

        resources.TryGetValue(ID, out result);
        return result;
    }
}


