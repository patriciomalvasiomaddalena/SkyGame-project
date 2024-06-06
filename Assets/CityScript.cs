using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScript : MonoBehaviour
{
    public enum CityType
    {
        city,
        FuelCity,
        WeaponCity,
        Shipyard,
        Info,
        Merc,
    }
    [SerializeField] LayerMask _CollLayer;
    [SerializeField] float _Timer;
    [SerializeField] Fleet_Player PlayerF;
    [SerializeField] bool Entered;


    [Header("City Modifiers")]
    public CityType CityMod;

    private void EnterCity(Fleet_Player PlayerF)
    {
        print("flota jugador: " + PlayerF.gameObject.name + " ha entrado a la ciudad " + this.gameObject.name);
        Entered = true;
        CampaignManager.ShopManagerInstance.SetPlayerFleetRef(PlayerF);
        CampaignManager.ShopManagerInstance.OpenCityCanvas(this.CityMod);
    }

    float pulse;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == _CollLayer || collision.CompareTag("PlayerFleet") && Entered == false) 
        {
            if(_Timer > pulse)
            {
                pulse = pulse + 1*Time.deltaTime;
            }
            else
            {
                if(collision.GetComponent<Fleet_Player>() != null && Entered == false)
                {
                    PlayerF = collision.GetComponent<Fleet_Player>();
                    EnterCity(PlayerF);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(PlayerF == null)
        {
            return;
        }

        if(collision == PlayerF.gameObject.GetComponent<Collider2D>())
        {
            Entered = false;
            PlayerF = null;
            pulse = 0;
            CampaignManager.ShopManagerInstance.CloseCityCanvas();
        }
        else
        {
            return;
        }
    }
}
