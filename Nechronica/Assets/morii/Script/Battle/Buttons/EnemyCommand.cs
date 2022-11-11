using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCommand : MonoBehaviour
{
    [SerializeField]
    private BattleSystem battleSystem;
    [SerializeField]
    private GetClickedGameObject getClickedObj;


    // Start is called before the first frame update
    void Start()
    {
        //battleSystem = ManagerAccessor.Instance.battleSystem;
        //getClickedObj = ManagerAccessor.Instance.getClickedObj;


    }

    void OnClickAtk()
    {

    }

    void SetClickAtk(CharaManeuver maneuver,int area)
    {

    }
}
