 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hull_Piece : MonoBehaviour
{
    protected object[] a = null;
    [SerializeField] protected InsiderManager _InsiderManager;
    [SerializeField] protected GameObject[] _NeighborSnapPoints = new GameObject[4];
    [SerializeField] protected List<Transform> _SnappedPoint = new List<Transform>();
    [SerializeField] protected ModuleBase _AttachedModule;
    public float _HP;
    public bool IsNPC;

    public HealthComponent _HealthComponent;

    private void Start()
    {
        _HealthComponent =GetComponent<HealthComponent>();
        _HealthComponent.OnDeathEvent += Death;
        _HealthComponent.SetHealth(_HP);
        _InsiderManager= GetComponentInParent<InsiderManager>();
        if (IsNPC)
        {
            _InsiderManager.SubscribeToEvent(InsiderEventType.NPC_Event_CommandDeath, CommandDeath);
        }
        else
        {
            _InsiderManager?.SubscribeToEvent(InsiderEventType.Event_CommandDeath, CommandDeath);
        }
        _InsiderManager?.TriggerEvent(InsiderEventType.Event_HullRepair);
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

    protected void Death()
    {
        DeathSubscriber(a);
        _AttachedModule?.DisabledModule();
        //this.gameObject.SetActive(false);
    }

    protected void DeathSubscriber(object[] p)
    {
        _InsiderManager?.TriggerEvent(InsiderEventType.Event_HullBroken);
    }

    protected void CommandDeath(object[] p)
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

    protected void CheckConnected()
    {
        if(_SnappedPoint.Count <= 0)
        {
            Death();
        }
    }
}
