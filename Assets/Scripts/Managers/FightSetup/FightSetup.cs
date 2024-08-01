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

    [SerializeField] float _CombatRadius, _SwitchSceneCooldown, _CooldownDuration, PlayerShipsLeft, NPCShipsLeft;
    [SerializeField] int NPCShipIndex, PlayerShipIndex;

    bool Fighting = false,FirstBorns = false;
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

        if(Fighting == false)
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
        FirstBorns = true;
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
        NextNPCSpawn(default);
        Fighting = true;

    }

    private void CheckForInjuries()
    {
        if(_EnemyFleetComp.Count <= 0)
        {
            foreach(Fleet_Enemy Fenemy in _EnemyFleetsInCombat)
            {
                Fenemy._FleetComposition.Clear();
            }
        }
        if (_PlayerFleetComp.Count <= 0)
        {
            foreach(Fleet_Player Fplayer in _PlayerFleetsInCombat)
            {
                Fplayer._FleetComposition.Clear();
            }
        }
    }
    public void NextNPCSpawn(object[] a)
    {
        if (_EnemyFleetComp[NPCShipIndex] == null)
        {
            Debug.LogError("NPCSpawn not Working");
            return;
        }
        if(NPCShipIndex > _EnemyFleetComp.Count)
        {
            return;
        }

        if (_EnemyFleetComp[NPCShipIndex].ShipBaseData.ShipBlueprint != null)
        {
            #region spawnLogic
            GameObject NewShip = Instantiate(_EnemyFleetComp[NPCShipIndex].ShipBaseData.ShipBlueprint, this.transform.position, Quaternion.identity);
            _EnemyFleetComp[NPCShipIndex].CurrentShipInstance = NewShip;
            NewShip.transform.SetParent(ScreenManager.Instance.ScreenDiccionary["IDFight"].gameObject.transform);

            #endregion
            NPCShipIndex++;
            NPCShipsLeft--;
        }
    }

    public void NextPlayerSpawn(object[] a)
    {
        for (int i = 0; i < _PlayerFleetsInCombat.Count; i++)
        {
            var wawa = _PlayerFleetsInCombat[i].GetComponent<Fleet_Player>();
            if (wawa._FleetComposition.Count > 0)
            {
                //wawa._FleetComposition.Remove(wawa._FleetComposition[0]);
            }
        }
        PlayerShipsLeft--;

    }
}
