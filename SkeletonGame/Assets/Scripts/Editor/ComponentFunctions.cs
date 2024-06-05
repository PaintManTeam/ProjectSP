using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Data;
using static Define;
using System.ComponentModel;

// 테스트 스크립트 (임시)
[CustomEditor(typeof(GimmickObject))]
public class GimmickCustomComponent : Editor
{
    AddComponentDataTable addComponentData = new AddComponentDataTable();

    EGimmickType gimmickType = EGimmickType.None;
    
    // test
    public GameObject testObj;
    public GameObject[] testObjArray;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GimmickObject gimmickObject = (GimmickObject)target;

        GUILayout.Label("Test Menu", EditorStyles.boldLabel);
        addComponentData.testInt = EditorGUILayout.IntField("TestInt", addComponentData.testInt);
        testObj = EditorGUILayout.ObjectField("TestObj:", testObj, typeof(GameObject), true) as GameObject;


        GUILayout.Label("Clear Component Menu", EditorStyles.boldLabel);

        if (GUILayout.Button("Clear Component All"))
        {
            gimmickObject.ClearCustomComponentAll();
        }


        GUILayout.Label("Add Component Menu", EditorStyles.boldLabel);

        gimmickType = (EGimmickType)EditorGUILayout.EnumPopup("기믹 타입 : ", gimmickType);

        /*
        so.Update();
        SerializedProperty titleProperty = so.FindProperty("TestObjArray");
        EditorGUILayout.PropertyField(titleProperty, true);
        so.ApplyModifiedProperties();
        */

        if (GUILayout.Button("Custom Add Component"))
        {
            if(gimmickType == EGimmickType.None)
            {
                Debug.LogError("기믹 타입이 설정되지 않았습니다.");
                return;
            }

            gimmickObject.CustomAddComponent(gimmickType);
        }

        GUILayout.Label("Save Component Menu", EditorStyles.boldLabel);

        if (GUILayout.Button("Custom Save Component"))
        {
            // 세이브 하는 순간에 최초 저장되는 아이라면, 고유 ID를 부여?
            gimmickObject.SaveComponentData();
        }

        GUILayout.Label("Load Component Menu", EditorStyles.boldLabel);

        if (GUILayout.Button("Custom Load Component"))
        {
            // 데이터 로드
            gimmickObject.LoadComponentData();
        }
    }

    [Serializable]
    public class AddComponentDataTable
    {
        [ReadOnly] public int testInt;
        public int testString;
    }
}