using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Field_change : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Field;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputField(Dropdown DD)
    {
        int a = DD.value;

        for(int i=0;i!=3;i++)
        {
            if (i == a)
                Field[i].SetActive(true);
            else
                Field[i].SetActive(false);
        }
    }


}
