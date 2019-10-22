using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveOri))]
public class ConVertTransToProAnim : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveOri saver = (SaveOri)target;

        if (GUILayout.Button("Save Data to Scriptable Object"))
        {
            saver.SaveData();
        }

        if (GUILayout.Button("Set Transform from Scriptable Object"))
        {
            saver.ChangeToData();
        }
    }
}
