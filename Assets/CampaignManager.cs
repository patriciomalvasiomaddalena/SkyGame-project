using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CampaignManager : MonoBehaviour
{
    public float PlayerCredits;

    public bool FleetSelected;

    public static CampaignManager Instance;

    public List<Fleet_Player> _PlayerFleets = new List<Fleet_Player>();
   
    public List<Fleet_Enemy> _EnemyFleets = new List<Fleet_Enemy>();

    public static ShopManager ShopManagerInstance;
    public Canvas _UICanvas;
    public UIManager uIManager;

    public TextMeshProUGUI _FuelCount,_CreditCount;

    [SerializeField] GameObject PlayerUI, ButtonLoseSelection;

    public GameObject JoystickUI;


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
        AudioManager.instance?.PlayMasterMusicAudio("ID_Radar");

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

    public void SelectFleet()
    {
        FleetSelected = true;
        if(config_manager._Instance.CurrentController != ControllerType.KyM)
        {
            ButtonLoseSelection.gameObject.SetActive(true);
        }
    }

    public void DeselectFleet()
    {
        foreach(Fleet_Player Pfleet in _PlayerFleets)
        {
            Pfleet.LoseSelection();
        }
        FleetSelected = false;
        ButtonLoseSelection.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(_PlayerFleets.Count <= 0)
        {
            GameManager.Instance.MainMenu();
        }
    }

}
