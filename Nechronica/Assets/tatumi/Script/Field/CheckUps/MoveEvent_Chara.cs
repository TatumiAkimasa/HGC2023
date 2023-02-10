using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent_Chara : MonoBehaviour
{
    //入れ替え対象元Camera
    private int InCamera = 0;

    //Classでまとめてるならいる
    [System.Serializable]
    public class EventOrders
    {
        //pubじゃねぇとEditor反応せず(拡張ができない)
        public int Horizon,Vertical,Jump_count,Move_Time,ObjChara_Num,Target;
        //もちろん中身もpublicで
        public string[] Talk;
        public float WaitTime;
       
    }

    [System.Serializable]
    public struct Events
    {
        public EventType.event_Type eventType;
        public EventOrders ordes;
    }
    
    //EditorでListの表示をしてるやつ
    [SerializeField, Header("イベント表")]
    public List<Events> EventTypes;

    //アシスタントさん
    private Assist_MoveEvent Assistcs;

    private void Awake()
    {
        //絶対SET
        Assistcs = this.gameObject.GetComponent<Assist_MoveEvent>();
    }

    //実際の処理(Editor関係なし)
    public IEnumerator Event(System.Action<bool> action_end)
    {
        for (int j = 0; j != Assistcs.Charas.Length; j++)
        {
            Assistcs.Charas[j].MovePlayer = false;
            Assistcs.Charas[j].myanim.SetEvent = true;
        }

        for (int i=0;i!=EventTypes.Count;i++)
        {
            switch (EventTypes[i].eventType)
            {
                case EventType.event_Type.Nomove:
                    yield return new WaitForSeconds(EventTypes[i].ordes.WaitTime);
                    break;
                case EventType.event_Type.HorizonMove:
                    yield return StartCoroutine(Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].Event_Horimove(EventTypes[i].ordes.Move_Time, EventTypes[i].ordes.Horizon));
                    yield return null;
                    break;
                case EventType.event_Type.VerticalMove:
                    yield return StartCoroutine(Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].Event_Vertimove(EventTypes[i].ordes.Move_Time, EventTypes[i].ordes.Vertical));
                    yield return null;
                    break;
                case EventType.event_Type.Horizonposture:
                    yield return StartCoroutine(Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].Event_Horiposture(EventTypes[i].ordes.Move_Time, EventTypes[i].ordes.Horizon));
                    yield return null;
                    break;
                case EventType.event_Type.Verticalposture:
                    yield return StartCoroutine(Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].Event_Vertiposture(EventTypes[i].ordes.Move_Time, EventTypes[i].ordes.Vertical));
                    yield return null;
                    break;
                case EventType.event_Type.JumpMove:
                    yield return StartCoroutine(Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].Event_jump(EventTypes[i].ordes.Jump_count, EventTypes[i].ordes.WaitTime));
                    yield return null;
                    break;
                case EventType.event_Type.TalkStart:
                    bool TalkEnd = false;
                    Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].gameObject.GetComponent<Talk_Chara>().Set_Talkstr(EventTypes[i].ordes.Talk);
                    Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].gameObject.GetComponent<Talk_Chara>().NameSetTalk();
                    yield return StartCoroutine(Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].gameObject.GetComponent<Talk_Chara>().Talk_Set((EndTiming=>
                    {
                        //何かしゃべりおわった時にしたいことあれば今は無し(多分PLの操作受付拒否解除処理とか入る)
                        TalkEnd = true;
                    })));
                    while(!TalkEnd)
                    yield return null;
                    break;
                case EventType.event_Type.CameraMove:
                    if (EventTypes[i].ordes.Target == 0)
                    {
                        Assistcs.ChinemaCameras[EventTypes[i].ordes.Target].SetActive(true);
                      
                        yield return new WaitForSeconds(3.0f);
                        Assistcs.ChinemaCameras[InCamera].SetActive(false);
                        InCamera = EventTypes[i].ordes.Target;
                    }
                    else
                    {
                        Assistcs.ChinemaCameras[EventTypes[i].ordes.Target].SetActive(true);
                        yield return new WaitForSeconds(0.01f); ;
                        Assistcs.ChinemaCameras[InCamera].SetActive(false);

                        yield return new WaitForSeconds(3.0f);
                        InCamera = EventTypes[i].ordes.Target;
                    }
                    break;
                case EventType.event_Type.DeleteChara:
                    Destroy(Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].gameObject);
                    break;
                case EventType.event_Type.SetObj:
                    Assistcs.SetObjs[EventTypes[i].ordes.Target].SetActive(true);
                    break;
                case EventType.event_Type.MusicSet:
                    Assistcs.audioSource.PlayOneShot(Assistcs.Audios[EventTypes[i].ordes.Target]);
                    break;
            }
        }
        action_end(true);
    }
    
}
