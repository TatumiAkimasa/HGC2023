using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Table_Parts))]
public class HelpBoxTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
      
        EditorGUILayout.HelpBox("Timing\nCOUNT  = -1\nAUTO = 0\nACTION = 1\nMOVE = 2\nRAPID = 3\nJUDGE = 4\nDAMAGE = 5", MessageType.Info);
       
    }
}