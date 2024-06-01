using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InsiderEventType
{
    Event_HullBroken,
    Event_CommandDeath,
    Event_HullRepair,
    NPC_Event_CommandDeath
}


public class InsiderManager : MonoBehaviour
{
    //este script será usado para comunicar eventos dentro de cada nave
    // Cada nave solo podra activar eventos de su mismo insider.

    [SerializeField] bool IsWorking = true; // solo esta para dejar claro que el script funciona (si no parece ser un script vacio)
  
    static Dictionary<InsiderEventType, MethodToSubscribe> InsiderEvents = new Dictionary<InsiderEventType, MethodToSubscribe>();

    public delegate void MethodToSubscribe(params object[] parameters);


    public void SubscribeToEvent(InsiderEventType eventType, MethodToSubscribe methodToSubscribe)
    {
        if (InsiderEvents == null) InsiderEvents = new Dictionary<InsiderEventType, MethodToSubscribe>();

        InsiderEvents.TryAdd(eventType, null);

        InsiderEvents[eventType] += methodToSubscribe;

        print("subscribed event" + eventType + methodToSubscribe.ToString());
    }

    public void UnSubscribeToEvent(InsiderEventType eventType, MethodToSubscribe methodToUnsubscribe)
    {
        if (InsiderEvents == null) return;

        if (!InsiderEvents.ContainsKey(eventType)) return;

        InsiderEvents[eventType] -= methodToUnsubscribe;
        Debug.Log("UnSubscribing Event" + methodToUnsubscribe.Method.Name);
    }

    public void TriggerEvent(InsiderEventType eventType, params object[] parameters)
    {
        if (InsiderEvents == null) return;
        if (!InsiderEvents.ContainsKey((InsiderEventType)eventType)) return;
        if (InsiderEvents[eventType] == null) return;
        InsiderEvents[eventType](parameters);
    }

}
