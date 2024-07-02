using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaTester : MonoBehaviour
{
    public float MaxStamina;
    public float CurrentStamina;
    public float RegenRate;
    [SerializeField]StaminaRegenComp _StaminaRegen;


    void Start()
    {
        _StaminaRegen = GetComponent<StaminaRegenComp>();

        _StaminaRegen._MaxStaminaCount= MaxStamina;
        _StaminaRegen._CurrentStamina= CurrentStamina;
        _StaminaRegen._StaminaRegen = RegenRate;
        // CurrentStamina = _StaminaRegen.RechargeStamina(CurrentStamina,MaxStamina,StaminaRegenComp.TimeScale.Seconds); 

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) 
        {
            CurrentStamina = _StaminaRegen.RechargeStamina(CurrentStamina, MaxStamina, StaminaRegenComp.TimeScale.Seconds);

        }


    }

}
