using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent_Player : MonoBehaviour
{
    [SerializeField]
    private EventMoveChara[] Charas;

    [SerializeField]
    private GameObject[] ChinemaCameras;
    //pubじゃねぇとEditor反応せず
    public int InCamera, OutCamera;

    [System.Serializable]
    public class EventOrders
    {
        //[System.NonSerialized]
        public int Horizon,Vertical,Jump_count,Move_Time,ObjChara_Num;
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
    

    [SerializeField, Header("イベント表")]
    public List<Events> EventTypes;

    public IEnumerator Event()
    {
        for(int i=0;i!=EventTypes.Count;i++)
        {
            switch (EventTypes[i].eventType)
            {
                case EventType.event_Type.Nomove:
                    yield return new WaitForSeconds(EventTypes[i].ordes.WaitTime);
                    break;
                case EventType.event_Type.HorizonMove:
                    yield return StartCoroutine(Charas[EventTypes[i].ordes.ObjChara_Num].Event_Horimove(EventTypes[i].ordes.Move_Time, EventTypes[i].ordes.Horizon));
                    yield return null;
                    break;
                case EventType.event_Type.VerticalMove:
                    yield return StartCoroutine(Charas[EventTypes[i].ordes.ObjChara_Num].Event_Vertimove(EventTypes[i].ordes.Move_Time, EventTypes[i].ordes.Vertical));
                    yield return null;
                    break;
                case EventType.event_Type.JumpMove:
                    yield return StartCoroutine(Charas[EventTypes[i].ordes.ObjChara_Num].Event_jump(EventTypes[i].ordes.Jump_count, EventTypes[i].ordes.WaitTime));
                    yield return null;
                    break;
                case EventType.event_Type.TalkStart:
                    Charas[EventTypes[i].ordes.ObjChara_Num].gameObject.GetComponent<Talk_Chara>().Set_Talkstr(EventTypes[i].ordes.Talk);
                    StartCoroutine(Charas[EventTypes[i].ordes.ObjChara_Num].gameObject.GetComponent<Talk_Chara>().Talk_Set((EndTiming=>
                    {
                        //何かしゃべりおわった時にしたいことあれば今は無し(多分PLの操作受付拒否解除処理とか入る)
                    })));
                    yield return null;
                    break;
                case EventType.event_Type.CameraMove:
                    ChinemaCameras[InCamera].SetActive(false);
                    ChinemaCameras[OutCamera].SetActive(true);
                    InCamera = OutCamera;
                    break;
            }

        }
    }

}
