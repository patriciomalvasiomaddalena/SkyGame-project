using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterBase : ModuleBase
{

    [SerializeField] float _ThrusterSpeed;
    movement _PlayerMovement;
    private void Start()
    {
        _PlayerMovement = FindObjectOfType<movement>();
        _PlayerMovement.AddSpeed(_ThrusterSpeed);
    }

    protected override void RunLogic()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("ModPoint") && _ModuleAttach == null)
        {
            _ModuleAttach = collision.gameObject;
            //this.transform.position = new Vector3(_ModuleAttach.transform.position.x, _ModuleAttach.transform.position.y, -1);
            this.gameObject.transform.SetParent(_ModuleAttach.transform, true);
            _ModuleAttach.GetComponentInParent<Hull_Piece>().ModuleAttach(this);

        }
    }


    public override void DisabledModule()
    {
        _PlayerMovement.AddSpeed(-_ThrusterSpeed);
    }
}
