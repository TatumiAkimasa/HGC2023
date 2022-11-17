using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent_Player : MonoBehaviour
{
    [System.Serializable]
    public class EventOrders
    {
        //[System.NonSerialized]
        public int Horizon,Vertical,Jump_count,Move_Time;
        //[System.NonSerialized]
        public string[] Talk;
        //[System.NonSerialized]
        public float WaitTime;
    }
    [System.Serializable]
    public struct Events
    {
        public EventType.event_Type eventType;
        //[System.NonSerialized]
        public EventOrders ordes;
    }

    [SerializeField, Header("‚±‚±‚Å‚â‚é‚©")]
    public List<Events> EventType;

  
}
