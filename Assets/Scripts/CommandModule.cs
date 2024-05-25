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
    InsiderManager.MethodToSubscribe NewMethods;
    bool ded = false;

    private void Start()
    { 
        

        _InsiderManager = GetComponentInParent<InsiderManager>();
        if(_InsiderManager != null )
        {
            _InsiderManager.SubscribeToEvent(InsiderEventType.Event_HullBroken, TakeShipDamage);
            _InsiderManager.SubscribeToEvent(InsiderEventType.Event_CommandDeath, TotalShipDeath);
            _InsiderManager.SubscribeToEvent(InsiderEventType.Event_HullRepair, TakeShipRepair);
        }
        else
        {
            Start();
        }
        
    }

    private void TotalShipDeath(object[] p)
    {
        ded = true;
        //_InsiderManager.TriggerEvent(InsiderEventType.Event_CommandDeath);
        //GameManager.Instance.ReturnToMenu();
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
            ded = true;
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
        Debug.Log("fuck u");
    }
}
