using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullMod_Fuel_Tank : ModuleBase
{
    [SerializeField] private float _FuelAmount;
    [SerializeField] private float _ExplosionRadius, _ExplosionDMG;
    [SerializeField] private float _HealthMalus;
    [SerializeField] private Vfx_Exp_Script _VFXScript;
    public override void DisabledModule()
    {
        AoEManager.AoECalculation(this.transform, _ExplosionRadius, _ExplosionDMG);
        _VFXScript.ActivateGMOB();
        _VFXScript.PlayVFX();
        this.gameObject.SetActive(false);
    }

    protected override void RunLogic()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("ModPoint") && _ModuleAttach == null)
        {
            _ModuleAttach = collision.gameObject;
            this.transform.position = new Vector3(_ModuleAttach.transform.position.x, _ModuleAttach.transform.position.y, -1);
            this.gameObject.transform.SetParent(_ModuleAttach.transform, true);
            GetComponentInParent<Hull_Piece>().ModuleAttach(this);

            if (_ModuleAttach != null)
            {
                _ModuleAttach.GetComponentInParent<Hull_Piece>()._HealthComponent.SetHealth(_HealthMalus);

                RunLogic();
            }
        }
    }


}
