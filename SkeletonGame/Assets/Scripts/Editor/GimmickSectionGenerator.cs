using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Define;

[CustomEditor(typeof(GimmickSection))]
public class GimmickSectionGenerator : Editor
{
    EGimmickCollisionObjectType collisionGimmickType;
    EGimmickInteractionObjectType interactionGimmickType;

    bool isLoadUnlocked = false;
    bool isSaveUnlocked = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GimmickSection gimmickSection = (GimmickSection)target;

        GUILayout.Space(10);
        GUILayout.Label("- 기믹 오브젝트 -", EditorStyles.boldLabel);

        // 데이터 저장
        GUILayout.Space(5);
        isSaveUnlocked = EditorGUILayout.Toggle("저장 잠금 해제", isSaveUnlocked);
        GUILayout.Space(5);
        if (GUILayout.Button("기믹 섹션 데이터 저장"))
            gimmickSection.SaveSectionData();

        // 데이터 불러오기
        GUILayout.Space(5);
        isLoadUnlocked = EditorGUILayout.Toggle("불러오기 잠금 해제", isLoadUnlocked);
        if (GUILayout.Button("기믹 섹션 데이터 불러오기"))
            gimmickSection.LoadSectionData();

        // 충돌 타입 추가
        GUILayout.Space(15);
        GUILayout.Label("충돌 타입 생성", EditorStyles.boldLabel);
        GUILayout.Space(5);
        collisionGimmickType = (EGimmickCollisionObjectType)EditorGUILayout.EnumPopup("충돌 타입 생성", collisionGimmickType);
        GUILayout.Space(5);
        if (GUILayout.Button("충돌 타입 생성"))
            gimmickSection.AddGimmickCollisionObject(collisionGimmickType);

        // 상호작용 타입 추가
        GUILayout.Space(15);
        GUILayout.Label("상호작용 타입 생성", EditorStyles.boldLabel);
        GUILayout.Space(5);
        interactionGimmickType = (EGimmickInteractionObjectType)EditorGUILayout.EnumPopup("충돌 타입 생성", interactionGimmickType);
        GUILayout.Space(5);
        if (GUILayout.Button("상호작용 타입 생성"))
            gimmickSection.GenerateGimmickInteractionObject(interactionGimmickType);

        GUILayout.Space(20);
    }
}
