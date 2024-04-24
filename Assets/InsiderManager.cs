using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public enum InsiderEventType
{
    Event_HullBroken,
}


public class InsiderManager : MonoBehaviour
{
    //este script será usado para comunicar eventos dentro de cada nave
    // Cada nave solo podra activar eventos de su mismo insider.

    [SerializeField] bool IsWorking = true; // solo esta para dejar claro que el script funciona (si no parece ser un script vacio)
  
    [SerializedDictionary("IDobject", "Object")]
    static SerializedDictionary<EventType, MethodToSubscribe> InsiderEvents = new SerializedDictionary<EventType, MethodToSubscribe>();

    public delegate void MethodToSubscribe(params object[] parameters);


    public void SubscribeToEvent(EventType eventType, MethodToSubscribe methodToSubscribe)
    {
        if (InsiderEvents == null) InsiderEvents = new SerializedDictionary<EventType, MethodToSubscribe>();

        InsiderEvents.TryAdd(eventType, null);

        InsiderEvents[eventType] += methodToSubscribe;
    }

    public static void UnSubscribeToEvent(EventType eventType, MethodToSubscribe methodToUnsubscribe)
    {
        if (InsiderEvents == null) return;

        if (!InsiderEvents.ContainsKey(eventType)) return;

        InsiderEvents[eventType] -= methodToUnsubscribe;
        Debug.Log("UnSubscribing Event" + methodToUnsubscribe.Method.Name);
    }

    public static void TriggerEvent(EventType eventType, params object[] parameters)
    {
        if (InsiderEvents == null) return;
        if (!InsiderEvents.ContainsKey((EventType)eventType)) return;
        if (InsiderEvents[eventType] == null) return;

        InsiderEvents[eventType](parameters);
    }

}
