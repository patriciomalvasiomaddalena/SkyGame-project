using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;

public enum IScreenActive
{
    ScreenCampaign,
    ScreenFight
}

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    [SerializeField] Stack<Iscreen> _ScreenStacks = new Stack<Iscreen>();

    [SerializedDictionary("Scene ID", "SceneComponentPrefab")]
    public SerializedDictionary<string, ScreenComponent> ScreenDiccionary;

    public IScreenActive activeScreen;

    public event Action SwitchedScene = delegate { }; 

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PushScreen("IDCampaign");
    }

    float pulse, timer = 3;
    public void PushScreen(Iscreen PushingScreen, bool hideScreen = true)
    {
        AsyncLoadManager._Instance.FadeOutManager.StartFadeOut();
        print("pushscreen");

        //si tengo una screen activa previamente
        if(_ScreenStacks.Count > 0)
        {
            //la desactivamos sin sacarla del stack
            _ScreenStacks.Peek().Deactivate(hideScreen);
        }

        //pusheamos la screen nueva
        _ScreenStacks.Push(PushingScreen);
        //activamos la screen nueva
        PushingScreen.Activate();
    }

    public void PushScreen(string ScreenID, bool hideScreen = true)
    {
        if (ScreenDiccionary.ContainsKey(ScreenID))
        {
            StartCoroutine(PushScreenAsync(ScreenID, hideScreen));
        }
    }

    private IEnumerator PushScreenAsync( string ScreenID, bool hideScreen)
    {
        Iscreen PushingScreen = ScreenDiccionary[ScreenID];

        if (_ScreenStacks.Count > 0)
        {
            //la desactivamos sin sacarla del stack
            if (_ScreenStacks.Peek() != null)
            {
                _ScreenStacks.Peek().Deactivate(hideScreen);
            }
            else
            {
                Debug.LogWarning("screenstack is null");
            }
        }

        SwitchedScene.Invoke();

        if(ScreenID == "IDCampaign") //hack para saber que escena estoy usando
        {
            activeScreen = IScreenActive.ScreenCampaign;
        }
        else
        {
            activeScreen = IScreenActive.ScreenFight;
        }

        AsyncLoadManager._Instance.FadeOutManager.StartFadeOut();

        yield return new WaitForSeconds(2);
        //pusheamos la screen nueva
        _ScreenStacks.Push(PushingScreen);

        //activamos la screen nueva
        if (!PushingScreen.ScreenComp.gameObject.activeSelf)
        {
            PushingScreen.ScreenComp.gameObject.SetActive(true);
            PushingScreen.Activate();
            Debug.Log("pushing screen" + PushingScreen);
        }
        else
        {
            Debug.LogWarning("ScreenID not found: " + ScreenID);
        }
        yield return null;
    }

    public void PopScreen()
    {
        //Si solo tenemos una screen en el stack, no se puede ejecutar este comando.
        //porque nos quedariamos sin screens
        if(_ScreenStacks.Count == 1) { return; }

        //pop de screen y ejecutamos su evento de "desaparecer"
        _ScreenStacks.Pop().Free();

        //peekeamos la siguiente screen y la activamos
        if(_ScreenStacks.Peek().ScreenComp.gameObject.activeSelf == false)
        {
            _ScreenStacks.Peek().ScreenComp.gameObject.SetActive(true);
        }
        _ScreenStacks.Peek().Activate();
        
    }
}
