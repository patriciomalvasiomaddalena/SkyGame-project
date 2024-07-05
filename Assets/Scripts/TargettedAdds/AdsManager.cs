using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    [SerializeField] string _AndroidAdsID = "5650758";
    [SerializeField] string _IosAdsID = "5650759";

    public RewardedAds _RewardedAdsScript;
    public AdsInitializer _AdsInitializer;

    [SerializeField] bool _UseAds;

    public static AdsManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null && Instance != this) 
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if(_UseAds)
        {
            _RewardedAdsScript.LoadRewardedAdd();
        }
    }

}
