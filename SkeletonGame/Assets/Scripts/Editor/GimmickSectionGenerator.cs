using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Define;

[CustomEditor(typeof(GimmickSection))]
public class GimmickSectionGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GimmickSection gimmickSection = (GimmickSection)target;

        GUILayout.Label("기믹 오브젝트 생성 메뉴", EditorStyles.boldLabel);

        if (GUILayout.Button("기믹 오브젝트 생성"))
        {
            Debug.Log("기믹 오브젝트 생성");
        }
    }
}
