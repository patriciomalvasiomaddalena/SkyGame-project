using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignManager : MonoBehaviour
{
    public float PlayerCredits { get; private set; }

    public static CampaignManager Instance;

    public List<Fleet_Player> _PlayerFleets = new List<Fleet_Player>();

    public List<Fleet_Enemy> _EnemyFleets = new List<Fleet_Enemy>();

    public static ShopManager ShopManagerInstance;
    public Canvas _UICanvas;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        if(ShopManagerInstance == null)
        {
            ShopManagerInstance = GetComponent<ShopManager>();
        }
    }

    public void AddPlayerCredits(float ValueToAdd)
    {
        PlayerCredits += ValueToAdd;
    }

    public void RemovePlayerCredits(float ValueToRemove)
    {
        PlayerCredits -= ValueToRemove;
    }

}
