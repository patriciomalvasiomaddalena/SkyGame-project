using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBase : MonoBehaviour
{

    [SerializeField] protected GameObject _ModuleAttach;

    protected virtual void RunLogic()
    {

    }

    public virtual void DisabledModule() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("ModPoint") && _ModuleAttach == null)
        {
            _ModuleAttach  = collision.gameObject;
            this.transform.position = new Vector3(_ModuleAttach.transform.position.x,_ModuleAttach.transform.position.y, -1);
            this.gameObject.transform.SetParent(_ModuleAttach.transform, true);
            GetComponentInParent<Hull_Piece>().ModuleAttach(this);
        }
    }

}
