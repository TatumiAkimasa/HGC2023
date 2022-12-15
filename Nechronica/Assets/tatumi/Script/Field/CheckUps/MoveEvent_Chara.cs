using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent_Chara : MonoBehaviour
{
    //����ւ��Ώی�Camera
    private int InCamera = 0;

    //Class�ł܂Ƃ߂Ă�Ȃ炢��
    [System.Serializable]
    public class EventOrders
    {
        //[System.NonSerialized]���������ǁA������g���o����
        public int Horizon,Vertical,Jump_count,Move_Time,ObjChara_Num;
        //������񒆐g��public��
        public string[] Talk;
        public float WaitTime;
        //pub����˂���Editor��������(�g�����ł��Ȃ�)
        public int OutCamera;
    }

    [System.Serializable]
    public struct Events
    {
        public EventType.event_Type eventType;
        public EventOrders ordes;
    }
    
    //Editor��List�̕\�������Ă���
    [SerializeField, Header("�C�x���g�\")]
    public List<Events> EventTypes;

    //�A�V�X�^���g����
    private Assist_MoveEvent Assistcs;

    private void Awake()
    {
        //���SET
        Assistcs = this.gameObject.GetComponent<Assist_MoveEvent>();
    }

    //���ۂ̏���(Editor�֌W�Ȃ�)
    public IEnumerator Event(System.Action<bool> action_end)
    {
        Debug.Log("?");

        for (int j = 0; j != Assistcs.Charas.Length; j++)
        {
            Assistcs.Charas[j].MovePlayer = false;
            Assistcs.Charas[j].myanim.SetEvent = true;
        }

        Debug.Log("!");

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
                case EventType.event_Type.JumpMove:
                    yield return StartCoroutine(Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].Event_jump(EventTypes[i].ordes.Jump_count, EventTypes[i].ordes.WaitTime));
                    yield return null;
                    break;
                case EventType.event_Type.TalkStart:
                    bool TalkEnd = false;
                    Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].gameObject.GetComponent<Talk_Chara>().Set_Talkstr(EventTypes[i].ordes.Talk);
                    yield return StartCoroutine(Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].gameObject.GetComponent<Talk_Chara>().Talk_Set((EndTiming=>
                    {
                        //��������ׂ肨��������ɂ��������Ƃ���΍��͖���(����PL�̑����t���ۉ��������Ƃ�����)
                        TalkEnd = true;
                    })));
                    while(!TalkEnd)
                    yield return null;
                    break;
                case EventType.event_Type.CameraMove:
                    Assistcs.ChinemaCameras[EventTypes[i].ordes.OutCamera].SetActive(true);
                    yield return new WaitForSeconds(0.01f); ;
                    Assistcs.ChinemaCameras[InCamera].SetActive(false);
                    
                    InCamera = EventTypes[i].ordes.OutCamera;
                    yield return new WaitForSeconds(3.0f);
                    break;
                case EventType.event_Type.DeleteChara:
                    Destroy(Assistcs.Charas[EventTypes[i].ordes.ObjChara_Num].gameObject);
                    break;
            }
        }
        action_end(true);
    }
    
}
