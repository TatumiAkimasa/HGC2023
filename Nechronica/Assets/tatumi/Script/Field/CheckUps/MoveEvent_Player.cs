using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent_Player : MonoBehaviour
{
    [System.Serializable]
    public class EventOrders
    {
        public int RightorLeft,UporDown,Jump_count,Move_Time;
        public string[] Talk;
    }

    public struct Events
    {
        public EventOrders ordes;
        public EventType.event_Type eventType;
    }


    [SerializeField, Header("‚±‚±‚Å‚â‚é‚©")]
    public List<Events> EventType;

}
