 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hull_Piece : MonoBehaviour
{
    object[] a = null;
    [SerializeField] InsiderManager _InsiderManager;
    [SerializeField] GameObject[] _NeighborSnapPoints = new GameObject[4];
    [SerializeField] List<Transform> _SnappedPoint = new List<Transform>();
    [SerializeField] ModuleBase _AttachedModule;
    public bool IsNPC;

    HealthComponent _HealthComponent;

    private void Start()
    {
 
        _HealthComponent = GetComponent<HealthComponent>();
        _HealthComponent.OnDeathEvent += Death;
        _InsiderManager= GetComponentInParent<InsiderManager>();
        if (IsNPC)
        {
            _InsiderManager.SubscribeToEvent(InsiderEventType.NPC_Event_CommandDeath, CommandDeath);
        }
        else
        {
            _InsiderManager.SubscribeToEvent(InsiderEventType.Event_CommandDeath, CommandDeath);
        }
        _InsiderManager.TriggerEvent(InsiderEventType.Event_HullRepair);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("triggered!");
        if (collision != null && collision.CompareTag("SnapPoint") && _SnappedPoint.Count == 0)
        {
            _SnappedPoint.Add(collision.transform);
            //this.transform.position = _SnappedPoint[0].transform.position;
        }
    }

    public void ModuleAttach(ModuleBase _ModuleScript)
    {
        if (_AttachedModule == null) 
        {
            _AttachedModule = _ModuleScript;
        }
    }

    private void Death()
    {
        DeathSubscriber(a);
        this.gameObject.SetActive(false);
        _AttachedModule?.DisabledModule();
    }

    private void DeathSubscriber(object[] p)
    {
        _InsiderManager.TriggerEvent(InsiderEventType.Event_HullBroken);
    }

    private void CommandDeath(object[] p)
    {
        if (IsNPC)
        {
            _InsiderManager.UnSubscribeToEvent(InsiderEventType.NPC_Event_CommandDeath, CommandDeath);
        }
        else
        {
            _InsiderManager.UnSubscribeToEvent(InsiderEventType.Event_CommandDeath, CommandDeath);
        }


        if (_AttachedModule is CommandModule)
        {
            return;
        }
        else
        {
            this._AttachedModule?.DisabledModule();
        }

    }

    private void CheckConnected()
    {
        if(_SnappedPoint.Count <= 0)
        {
            Death();
        }
    }
}
