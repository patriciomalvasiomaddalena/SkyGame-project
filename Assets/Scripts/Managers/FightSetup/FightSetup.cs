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

    [SerializeField] float _CombatRadius;
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

    public void DragAllFleets(Transform CombatPosition)
    {
        Debug.Log("DragAllFleets");

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
    }

}
