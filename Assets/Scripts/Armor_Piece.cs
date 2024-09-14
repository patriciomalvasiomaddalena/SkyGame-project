using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor_Piece : Hull_Piece
{

    private void Start()
    {
        _HealthComponent = GetComponent<HealthComponent>();
        _HealthComponent.OnDeathEvent += Death;
        _HealthComponent.SetHealth(_HP);
        _InsiderManager = GetComponentInParent<InsiderManager>();
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

}
