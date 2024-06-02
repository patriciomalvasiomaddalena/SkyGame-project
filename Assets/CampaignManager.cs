using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignManager : MonoBehaviour
{
    public static CampaignManager Instance;

    public List<Fleet_Player> _PlayerFleets = new List<Fleet_Player>();

    public List<Fleet_Enemy> _EnemyFleets = new List<Fleet_Enemy>();

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
    }




}
