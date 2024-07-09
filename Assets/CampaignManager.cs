using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CampaignManager : MonoBehaviour
{
    public float PlayerCredits;

    public static CampaignManager Instance;

    public List<Fleet_Player> _PlayerFleets = new List<Fleet_Player>();
   
    public List<Fleet_Enemy> _EnemyFleets = new List<Fleet_Enemy>();

    public static ShopManager ShopManagerInstance;
    public Canvas _UICanvas;
    public UIManager uIManager;

    public TextMeshProUGUI _FuelCount,_CreditCount;

    [SerializeField] GameObject PlayerUI;



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
    private void Start()
    {
        UIManager.Instance.SetTMP("CreditTMP", "Credits: " + PlayerCredits);   
    }

    public void AddPlayerCredits(float ValueToAdd)
    {
        PlayerCredits += ValueToAdd;
        UIManager.Instance.SetTMP("CreditTMP", "Credits: " + PlayerCredits);
    }

    public void RemovePlayerCredits(float ValueToRemove)
    {
        PlayerCredits -= ValueToRemove;
        UIManager.Instance.SetTMP("CreditTMP", "Credits: " + PlayerCredits);
    }

    public void TogglePlayerUI(bool Toggle)
    {
        PlayerUI.SetActive(Toggle);
    }

}
