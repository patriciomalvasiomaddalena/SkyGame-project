using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FightSetup : MonoBehaviour
{
    public static FightSetup _Instance;

    public List<ShipScriptableOBJ> _PlayerFleetComp;

    public List<ShipScriptableOBJ> _EnemyFleetComp;

    public List<Fleet_Player> _PlayerFleetsInCombat;

    public List<Fleet_Enemy> _EnemyFleetsInCombat;

    private List<GameObject> _ShipsInCombat;

    [SerializeField] float _CombatRadius, _SwitchSceneCooldown, _CooldownDuration, PlayerShipsLeft, NPCShipsLeft;

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

        if (Fighting == true && NPCShipsLeft <= 0 || PlayerShipsLeft <= 0 && (_EnemyFleetsInCombat.Count > 0 && _PlayerFleetsInCombat.Count > 0))
        {
            CheckForInjuries();
            ScreenManager.Instance.PopScreen();
        }
    }

    public void DragAllFleets(Transform CombatPosition)
    {
        ClearVariables();
        hits = Physics2D.OverlapCircleAll(CombatPosition.position,_CombatRadius);
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
        if (_PlayerFleetsInCombat.Count < 0 || _EnemyFleetsInCombat.Count < 0)
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
    }

    private void GetPlayerCompositions()
    {
        foreach(Fleet_Player PlayerF in _PlayerFleetsInCombat)
        {
            foreach(ShipScriptableOBJ SCOB in PlayerF._FleetComposition)
            {
                _PlayerFleetComp.Add(SCOB);
            }
        }

        foreach(Fleet_Enemy EnemyF in _EnemyFleetsInCombat)
        {
            foreach(ShipScriptableOBJ SCEB in EnemyF._FleetComposition)
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
         
    }

    public void NextNPCSpawn(object[] a)
    {
        for(int i = 0;i < _EnemyFleetsInCombat.Count; i++)
        {
            var wawa = _EnemyFleetsInCombat[i].GetComponent<Fleet_Enemy>();
            if(wawa._FleetComposition.Count > 0)
            {
               SpawnNPCShip(wawa);
               wawa._FleetComposition.RemoveAt(0);
            }
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

    private void SpawnNPCShip(Fleet_Enemy EnemyFleet)
    {
        GameObject NewShip = Instantiate(EnemyFleet._FleetComposition[0].ShipBlueprint, this.transform.position, Quaternion.identity);
        EnemyFleet._FleetComposition[0].ShipObjectReference = NewShip;
        NewShip.transform.SetParent(ScreenManager.Instance.ScreenDiccionary["IDFight"].gameObject.transform);
        NPCShipsLeft--;
    }


}
