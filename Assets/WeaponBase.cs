using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : ModuleBase
{
    private void FixedUpdate()
    {
        RunLogic();
    }

    protected override void RunLogic()
    {

        Debug.Log("yep");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("ModPoint"))
        {
            _ModuleAttach = collision.gameObject;
            this.transform.position = new Vector3(_ModuleAttach.transform.position.x, _ModuleAttach.transform.position.y, -1);
            this.gameObject.transform.SetParent(_ModuleAttach.transform, true);

        }
    }

}
