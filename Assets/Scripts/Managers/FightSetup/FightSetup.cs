
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FightSetup : MonoBehaviour
{
    public static FightSetup _Instance;

    public List<ShipData> _PlayerFleetComp;

    public List<ShipData> _EnemyFleetComp;

    public List<Fleet_Player> _PlayerFleetsInCombat;

    public List<Fleet_Enemy> _EnemyFleetsInCombat;

    public GameObject _JoystickUI;

    [SerializeField] float _CombatRadius, _SwitchSceneCooldown, _CooldownDuration, PlayerShipsLeft, NPCShipsLeft;
    [SerializeField] int NPCShipIndex, PlayerShipIndex;

    bool Fighting = false;

    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    [SerializeField] Collider2D[] hits = new Collider2D[10];

    private void Start()
    {
        EventManager.SubscribeToEvent(EventType.Enemy_Ship_Lost, NextNPCSpawn);
        EventManager.SubscribeToEvent(EventType.Player_Ship_Lost, NextPlayerSpawn);  
    }

    private void Update()
    {
        if(_SwitchSceneCooldown > 0 )
        {
            _SwitchSceneCooldown = _SwitchSceneCooldown - 1 * Time.deltaTime;
        }
        if (_JoystickUI == null)
        {
            _JoystickUI = CampaignManager.Instance.JoystickUI;
        }
        if (Fighting == false)
        {
            return;
        }


        if (Fighting == true && (NPCShipsLeft <= 0 || PlayerShipsLeft <= 0) && (_EnemyFleetsInCombat.Count > 0 && _PlayerFleetsInCombat.Count > 0))
        {
            CheckForInjuries();
            ScreenManager.Instance.PopScreen();
        }
    }

    public void DragAllFleets(Transform CombatPosition)
    {
        ClearVariables();
        hits = Physics2D.OverlapCircleAll(CombatPosition.position,_CombatRadius);
        print("draging all fleets");
        foreach (Collider2D _CurrentCol in hits)
        {
            Debug.Log("checking all collisions");
           if(_CurrentCol.TryGetComponent(out Fleet_Base FleetType))
           {
                Debug.Log("Is FleetBase");
                if(FleetType is Fleet_Player)
                {
                    _PlayerFleetsInCombat.Add(_CurrentCol.GetComponent<Fleet_Player>());
                }
                else if(FleetType is Fleet_Enemy)
                {
                    _EnemyFleetsInCombat.Add(_CurrentCol.GetComponent<Fleet_Enemy>());
                }
           }
        }
        if (_PlayerFleetsInCombat.Count <= 0 || _EnemyFleetsInCombat.Count <= 0)
        {
            return;
        }
        else
        {
            GetPlayerCompositions();
        }
    }

    private void ClearVariables()
    {
        _EnemyFleetComp.Clear();
        _EnemyFleetsInCombat.Clear();
        _PlayerFleetComp.Clear();
        _PlayerFleetsInCombat.Clear();
        NPCShipIndex = 0;
        PlayerShipIndex = 0;
    }

    private void GetPlayerCompositions()
    {
        foreach(Fleet_Player PlayerF in _PlayerFleetsInCombat)
        {
            foreach(ShipData SCOB in PlayerF._FleetComposition)
            {
                _PlayerFleetComp.Add(SCOB);
            }
           
        }

        foreach(Fleet_Enemy EnemyF in _EnemyFleetsInCombat)
        {
            
            foreach(ShipData SCEB in EnemyF._FleetComposition)
            {
                _EnemyFleetComp.Add(SCEB);
            }
            
        }

        if(_SwitchSceneCooldown <= 0)
        {
            FightTime();
        }
    }

    private void FightTime()
    {
        NPCShipsLeft = _EnemyFleetComp.Count;
        PlayerShipsLeft = _PlayerFleetComp.Count;
        ScreenManager.Instance.PushScreen("IDFight");
        FirstTime = true;
        playerFirstTime = true;
        NextNPCSpawn(default);
        Fighting = true;

        if(_JoystickUI == null)
        {
            return;
        }

        if (config_manager._Instance.CurrentController != ControllerType.KyM)
        {
            _JoystickUI.SetActive(true);
<<<<<<< Updated upstream
=======
            config_manager._Instance.JoystickMovement.transform.parent.gameObject.SetActive(false);
>>>>>>> Stashed changes
        }
        else
        {
            _JoystickUI.SetActive(false);
        }

    }

    private void CheckForInjuries()
    {
        if(NPCShipsLeft <= 0)
        {
            foreach(Fleet_Enemy Fenemy in _EnemyFleetsInCombat)
            {
                Fenemy._FleetComposition.Clear();
            }
        }
        if (PlayerShipsLeft <= 0)
        {
            foreach(Fleet_Player Fplayer in _PlayerFleetsInCombat)
            {
                Fplayer._FleetComposition.Clear();
            }
        }
    }

    bool FirstTime = true;
    public void NextNPCSpawn(object[] a)
    {
        if (NPCShipIndex >= _EnemyFleetComp.Count)
        {
            NPCShipsLeft = -1;
            NPCShipIndex = 0;
            return;
        }
        if (_EnemyFleetComp[NPCShipIndex] == null)
        {
            Debug.LogError("NPCSpawn not Working");
            return;
        }

        if (_EnemyFleetComp[NPCShipIndex].ShipBaseData.ShipBlueprint != null)
        {
            #region spawnLogic
            GameObject NewShip = Instantiate(_EnemyFleetComp[NPCShipIndex].ShipBaseData.ShipBlueprint, this.transform.position, Quaternion.identity);
            _EnemyFleetComp[NPCShipIndex].CurrentShipInstance = NewShip;
            NewShip.transform.SetParent(ScreenManager.Instance.ScreenDiccionary["IDFight"].gameObject.transform);
            if(FirstTime == true && NPCShipIndex == 0)
            {
                NPCShipIndex++;
            }
            else if(FirstTime == true && NPCShipIndex >= 1)
            {
                FirstTime = false;
                NPCShipsLeft--;
            }

            if(FirstTime == false)
            {
                NPCShipIndex++;
                NPCShipsLeft--;
            }
     
            #endregion

        }
    }

    bool playerFirstTime = true;
    public void NextPlayerSpawn(object[] a)
    {
        if (PlayerShipIndex >= _PlayerFleetComp.Count)
        {
            PlayerShipsLeft = -1;
            PlayerShipIndex = 0;
            return;
        }

        if (_PlayerFleetComp[PlayerShipIndex] == null)
        {
            Debug.LogError("Player fleet composition doesnt have index");
            return;
        }

        if (_PlayerFleetComp[PlayerShipIndex].ShipBaseData != null)
        {
            #region SpawnShips
            GameObject NewPlayerShip = Instantiate(_PlayerFleetComp[PlayerShipIndex].ShipBaseData.ShipBlueprint, this.transform.position, Quaternion.identity);
            _PlayerFleetComp[PlayerShipIndex].CurrentShipInstance = NewPlayerShip;
            NewPlayerShip.transform.SetParent(ScreenManager.Instance.ScreenDiccionary["IDFight"].gameObject.transform);
            GameManager.Instance.PlayerShip = NewPlayerShip;

            if(playerFirstTime == true && PlayerShipIndex == 0)
            {
                PlayerShipIndex++;
            }
            else if(playerFirstTime == true && PlayerShipIndex >= 1)
            {
                playerFirstTime = false;
                PlayerShipsLeft--;
            }

            if (playerFirstTime == false)
            {
                PlayerShipIndex++;
                PlayerShipsLeft--;
            }
            #endregion
        }


    }
}
