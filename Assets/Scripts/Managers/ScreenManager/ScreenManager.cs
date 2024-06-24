using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    [SerializeField] Stack<Iscreen> _ScreenStacks = new Stack<Iscreen>();

    [SerializedDictionary("Scene ID", "SceneComponentPrefab")]
    public SerializedDictionary<string, ScreenComponent> ScreenDiccionary;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        PushScreen(ScreenDiccionary["IDCampaign"]);
    }

    public void PushScreen(Iscreen PushingScreen)
    {
        //si tengo una screen activa previamente
        if(_ScreenStacks.Count > 0)
        {
            //la desactivamos sin sacarla del stack
            _ScreenStacks.Peek().Deactivate();
        }

        //pusheamos la screen nueva
        _ScreenStacks.Push(PushingScreen);
        //activamos la screen nueva
        PushingScreen.Activate();

        print("pushed scene" + PushingScreen.ToString());
    }

    public void PushScreen(string ScreenID)
    {
        if(ScreenDiccionary.ContainsKey(ScreenID))
        {
            Iscreen PushingScreen = ScreenDiccionary[ScreenID];

            if (_ScreenStacks.Count > 0)
            {
                //la desactivamos sin sacarla del stack
                _ScreenStacks.Peek().Deactivate();
            }

            //pusheamos la screen nueva
            _ScreenStacks.Push(PushingScreen);

            //activamos la screen nueva
            if(!PushingScreen.ScreenComp.gameObject.activeSelf) 
            {
                PushingScreen.ScreenComp.gameObject.SetActive(true);
                PushingScreen.Activate();
            }

            Debug.Log("pushing screen" + PushingScreen);
        }
        else
        {
            Debug.LogWarning("ScreenID not found: " + ScreenID);
        }

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
