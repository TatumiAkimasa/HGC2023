using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MoveEvent_Charaのアシスト係
public class Assist_MoveEvent : MonoBehaviour
{
    //いかんせんEditor君ListやらGameObject認識やらしんどいので別Scriptで管理
    public EventMoveChara[] Charas;
    public GameObject[] ChinemaCameras;
    public GameObject[] SetObjs;
    public AudioClip[] Audios;
    public AudioSource audioSource;
}
