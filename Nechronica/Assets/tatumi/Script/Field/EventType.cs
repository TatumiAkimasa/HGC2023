using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventType
{
    [System.Serializable]
    public enum event_Type
    {
        Nomove = 0,
        RightMove,
        LeftMove,
        UpMove,
        DownMove,
        JumpMove,
        TalkStart,
        TalkEnd,
    }
}
