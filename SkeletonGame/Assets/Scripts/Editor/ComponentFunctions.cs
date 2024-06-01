using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Data;
using static Define;

[CustomEditor(typeof(GimmickObject))]
[CanEditMultipleObjects]
public class GimmickCustomComponent : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GimmickObject gimmickObject = (GimmickObject)target;

        if (GUILayout.Button("Custom Add Component"))
        {
            gimmickObject.CustomAddComponent();
        }

        if (GUILayout.Button("Custom Save Component"))
        {
            gimmickObject.SaveComponentData();
        }

        if (GUILayout.Button("Custom Load Component"))
        {
            gimmickObject.LoadComponentData();
        }
    }
}