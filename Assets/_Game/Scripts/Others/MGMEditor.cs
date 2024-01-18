using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MyGameManager))]
public class MGMEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Start Another Game"))
        {
            
            target.GetComponent<MyGameManager>().StartGame();
        }
    }
}
