 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandModule : ModuleBase
{
    object[] a =null;
    [SerializeField] float _ShipHealth;
    [SerializeField]InsiderManager _InsiderManager;
    public delegate void TotalShipDeathDel();
    public delegate void ShipDMGDel();
    public delegate void ShipRepDel();
    bool ded = false;
    public bool IsNPC;

    private void Start()
    { 
        _InsiderManager = GetComponentInParent<InsiderManager>();
        if(_InsiderManager != null )
        {
            if (IsNPC)
            {
                _InsiderManager.SubscribeToEvent(InsiderEventType.NPC_Event_CommandDeath, TotalShipDeath);
            }
            else
            {
                _InsiderManager.SubscribeToEvent(InsiderEventType.Event_CommandDeath, TotalShipDeath);
                _InsiderManager.SubscribeToEvent(InsiderEventType.Event_HullBroken, TakeShipDamage);
                _InsiderManager.SubscribeToEvent(InsiderEventType.Event_HullRepair, TakeShipRepair);
            }
        }
        else
        {
            Start();
        }
        
    }

    private void TotalShipDeath(object[] p)
    {
        if(ded == false)
        {
            ded = true;
            if (IsNPC)
            {
                _InsiderManager.TriggerEvent(InsiderEventType.NPC_Event_CommandDeath);
                EventManager.TriggerEvent(EventType.Enemy_Ship_Lost);
                GameObject Yippie = _InsiderManager.gameObject;
                Destroy(Yippie);
            }
            else
            {
                _InsiderManager.TriggerEvent(InsiderEventType.Event_CommandDeath);
                EventManager.TriggerEvent(EventType.Player_Ship_Lost);
                GameObject Yippie = _InsiderManager.gameObject;
                Destroy(Yippie);
            }
        }
    }

    private void TakeShipDamage(object[] p)
    {
        _ShipHealth--;
    }

    private void TakeShipRepair(object[] p)
    {
        _ShipHealth++;
    }
    public override void DisabledModule()
    {
        if(ded == false)
        {
            TotalShipDeath(default);
        }
        else
        {
            return;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("ModPoint") && _ModuleAttach == null)
        {
            _ModuleAttach = collision.gameObject;
            this.transform.position = new Vector3(_ModuleAttach.transform.position.x, _ModuleAttach.transform.position.y, -1);
            this.gameObject.transform.SetParent(_ModuleAttach.transform, true);
            GetComponentInParent<Hull_Piece>().ModuleAttach(this);
        }
    }
}
