using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string AndroidGameID;
    [SerializeField] string IosGameID;
    [SerializeField] bool TestingMode;

    private string GameID;

    private void Awake()
    {
        #if UNITY_IOS
        GameID = IosGameID;
        #elif UNITY_ANDROID
                GameID = AndroidGameID;
#elif UNITY_EDITOR
        GameID = AndroidGameID;
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(GameID, TestingMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Ads Initialized");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }

}
