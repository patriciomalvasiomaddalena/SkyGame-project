 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hull_Piece : MonoBehaviour
{
    [SerializeField] GameObject[] _NeighborSnapPoints = new GameObject[4];
    [SerializeField] List<Transform> _SnappedPoint = new List<Transform>();
    [SerializeField] ModuleBase _AttachedModule;

    HealthComponent _HealthComponent;

    private void Awake()
    {
        
    }

    private void Start()
    {
        _HealthComponent = GetComponent<HealthComponent>();
        _HealthComponent.OnDeathEvent += Death;
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
        _AttachedModule.DisabledModule();
        this.gameObject.SetActive(false);
    }

    private void CheckConnected()
    {
        if(_SnappedPoint.Count <= 0)
        {
            Death();
        }
    }
}
