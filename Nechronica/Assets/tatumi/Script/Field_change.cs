using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Field_change : MonoBehaviour
{
    [SerializeField]
    private GameObject Field;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputField()
    {
        Field.SetActive(true);
    }

    public void BackField()
    {
        Field.SetActive(false);
    }

}
