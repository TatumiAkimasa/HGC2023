using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventType
{
    //Ç±Ç¡ÇøÇ‡Ç¢ÇÈÇÊÅI
    [System.Serializable]
    public enum event_Type
    {
        Nomove = 0,
        HorizonMove,
        VerticalMove,
        JumpMove,
        TalkStart,
        CameraMove,
    }
}
