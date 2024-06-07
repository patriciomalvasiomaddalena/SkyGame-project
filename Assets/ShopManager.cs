using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Canvas ShopCanvas;
    [SerializeField] Fleet_Player Pfleet;


    [Header("Price of Items")]
    [SerializeField] float _FuelBasePrice, _FuelMod;

    [Header("References")]
    [SerializeField] Slider _FuelSlider;

    public void OpenCityCanvas(CityScript.CityType Modifiers)
    {
        ConfigureCityType(Modifiers);
        ShopCanvas.gameObject.SetActive(true);
        _FuelSlider.maxValue = Pfleet.MaxFuel;
        _FuelSlider.minValue = Pfleet.FuelAmount;
        _FuelSlider.value = Pfleet.FuelAmount / Pfleet.MaxFuel;
    }

    public void CloseCityCanvas()
    {
        ShopCanvas.gameObject.SetActive(false);
        RemovePlayerFleetRef();
    }

    public int FuelBought;
    public void SetFuelBuyAmount(float FuelPercentage)
    {
        float Buyfuel = FuelPercentage;
        float FuelTank = Pfleet.MaxFuel;
        if(Buyfuel > FuelTank)
        {
            Buyfuel = FuelTank;
        }

        FuelBought = (int)Buyfuel;
    }


    public void BuyFuel()
    {
        if(CampaignManager.Instance.PlayerCredits > ((_FuelMod * _FuelBasePrice)) * FuelBought)
        {
            if(Pfleet.FuelAmount + FuelBought > Pfleet.MaxFuel)
            {
                FuelBought = (int)(Pfleet.MaxFuel - Pfleet.FuelAmount);
            }


            float cost = (_FuelMod * _FuelBasePrice) * FuelBought;
            CampaignManager.Instance.RemovePlayerCredits(cost);

            Pfleet.FuelAmount += FuelBought;
            _FuelSlider.value = Pfleet.FuelAmount;
            _FuelSlider.minValue = Pfleet.FuelAmount;
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
