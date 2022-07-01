using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MousePos : MonoBehaviour, IPointerClickHandler
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Debug.Log("x:" + mousePos.x + "    y:" + mousePos.y);
        }
    }

    //Clickされた時に呼び出されるメソッド
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(this.name);
    }
}
