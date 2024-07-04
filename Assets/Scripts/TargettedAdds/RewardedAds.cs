using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Rendering.UI;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string AndroidAdUnitId;
    [SerializeField] string IosAdUnitId;

    [SerializeField] float FullReward, PartialReward;

   [SerializeField] string AD_ID;

    private void Awake()
    {
            #if UNITY_ANDROID
                    AD_ID = AndroidAdUnitId;
            #elif UNITY_IOS
            AD_ID = IosaDUnitID;
            #endif
    }



    public void LoadRewardedAdd()
    {
        Advertisement.Load(AD_ID, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(AD_ID, this);
        LoadRewardedAdd();
    }



    #region loadAd
    public void OnUnityAdsAdLoaded(string placementId)
    {
        
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogWarning("RewardedAd failed to load");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("ad event working");
        if(placementId == AD_ID)
        {
            Debug.Log("Ad_ID == PLACEMENTID");
        }
 

        if(placementId == AD_ID && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("full reward");
            CampaignManager.Instance.AddPlayerCredits(FullReward);
        }
        else if (placementId == AD_ID && showCompletionState == UnityAdsShowCompletionState.SKIPPED)
        {
            Debug.Log("partial reward");
            CampaignManager.Instance.AddPlayerCredits(PartialReward);
        }
        else if(placementId == AD_ID && showCompletionState == UnityAdsShowCompletionState.UNKNOWN)
        {
            Debug.Log("unknow status");
        }

    }
    #endregion
}
