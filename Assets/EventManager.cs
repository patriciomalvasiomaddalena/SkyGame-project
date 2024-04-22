using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Event_HullBroken,
}

public class EventManager : MonoBehaviour
{
    [SerializeField] float Testing;
    [SerializedDictionary("IDobject", "Object")] 
    static SerializedDictionary<EventType,MethodToSubscribe> EventsDictionary = new SerializedDictionary<EventType, MethodToSubscribe>();

    public delegate void MethodToSubscribe(params object[] parameters);


    public static void SubscribeToEvent(EventType eventType, MethodToSubscribe methodToSubscribe)
    {
        if(EventsDictionary == null) EventsDictionary = new SerializedDictionary<EventType,MethodToSubscribe>();

        EventsDictionary.TryAdd(eventType, null);

        EventsDictionary[eventType] += methodToSubscribe;
    }

    public static void UnSubscribeToEvent(EventType eventType, MethodToSubscribe methodToUnsubscribe)
    {
        if (EventsDictionary == null) return;

        if (!EventsDictionary.ContainsKey(eventType)) return;

        EventsDictionary[eventType] -= methodToUnsubscribe;
        Debug.Log("UnSubscribing Event" + methodToUnsubscribe.Method.Name);
    }

    public static void TriggerEvent(EventType eventType, params object[] parameters)
    {
        if(EventsDictionary == null) return;
        if(!EventsDictionary.ContainsKey((EventType)eventType)) return;
        if (EventsDictionary[eventType] == null) return;

        EventsDictionary[eventType](parameters);
    }
}
