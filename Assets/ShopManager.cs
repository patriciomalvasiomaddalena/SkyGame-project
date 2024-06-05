using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Canvas ShopCanvas;
    [SerializeField] Fleet_Player Pfleet;


    [Header("Price of Items")]
    [SerializeField] float _FuelBasePrice, _FuelMod;

    public void OpenCityCanvas(CityScript.CityType Modifiers)
    {
        ConfigureCityType(Modifiers);
        ShopCanvas.gameObject.SetActive(true);
    }

    public void CloseCityCanvas()
    {
        ShopCanvas.gameObject.SetActive(false);
        RemovePlayerFleetRef();
    }

    public void BuyFuel(float quantity)
    {
        if(CampaignManager.Instance.PlayerCredits > ((_FuelMod * _FuelBasePrice) * quantity))
        {
            float cost = (_FuelMod * _FuelBasePrice) * quantity;
            CampaignManager.Instance.RemovePlayerCredits(cost);
            Pfleet.FuelAmount += quantity;
        }
    }

    public void SetPlayerFleetRef(Fleet_Player fleet_Player)
    {
        Pfleet = fleet_Player;
    }

    public void RemovePlayerFleetRef()
    {
        Pfleet = null;
    }

    private void ConfigureCityType(CityScript.CityType Config)
    {
        switch (Config)
        {
            case CityScript.CityType.city:
                _FuelMod = 1.0f;
                break;
            case CityScript.CityType.FuelCity:
                _FuelMod = 0.5f;
                break;
        }
    }


}
