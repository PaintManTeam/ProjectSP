using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Define;

[CustomEditor(typeof(GimmickSection))]
public class GimmickSectionGenerator : Editor
{
    // 상호작용 타입
    EGimmickInteractionObjectType interactionGimmickType;
    string interactionObjectName = "";
    Sprite interactionObjectSprite;

    // 충돌 타입
    EGimmickCollisionObjectType collisionGimmickType;
    string collisionObjectName = "";
    Sprite collisionObjectSprite;

    // 삭제 기능
    int removeIndex = 0;

    // 키 잠금
    bool isLoadUnlocked = false;
    bool isSaveUnlocked = false;
    bool isRemoveUnlocked = false;

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
        if (GUILayout.Button("기믹 섹션 데이터 저장") && isSaveUnlocked)
        {
            gimmickSection.SaveSectionData();
            isSaveUnlocked = false;
            Debug.LogWarning("기믹 섹션 데이터 저장 요청 완료\n시간이 걸릴 수 있습니다.!! 바로 종료하지 마세요.!");
        }

        // 데이터 불러오기
        GUILayout.Space(5);
        isLoadUnlocked = EditorGUILayout.Toggle("불러오기 잠금 해제", isLoadUnlocked);
        if (GUILayout.Button("기믹 섹션 데이터 불러오기") && isLoadUnlocked)
        {
            gimmickSection.LoadSectionData();
            isLoadUnlocked = false;
            Debug.Log("기믹 섹션 데이터 불러오기 완료");
        }

        // 상호작용 타입 추가
        GUILayout.Space(15);
        GUILayout.Label("상호작용 타입 생성", EditorStyles.boldLabel);
        GUILayout.Space(5);
        interactionObjectName = EditorGUILayout.TextField("생성 시 이름 설정", interactionObjectName);
        GUILayout.Space(5);
        interactionGimmickType = (EGimmickInteractionObjectType)EditorGUILayout.EnumPopup("상호작용 타입 설정", interactionGimmickType);
        GUILayout.Space(5);
        interactionObjectSprite = (Sprite)EditorGUILayout.ObjectField("오브젝트 기본 이미지", interactionObjectSprite, typeof(Sprite), true);
        GUILayout.Space(5);
        if (GUILayout.Button("상호작용 타입 생성"))
            gimmickSection.GenerateGimmickInteractionObject(interactionGimmickType, interactionObjectName, interactionObjectSprite);

        // 충돌 타입 추가
        GUILayout.Space(15);
        GUILayout.Label("충돌 타입 생성", EditorStyles.boldLabel);
        GUILayout.Space(5);
        collisionObjectName = EditorGUILayout.TextField("생성 시 이름 설정", collisionObjectName); 
        GUILayout.Space(5);
        collisionGimmickType = (EGimmickCollisionObjectType)EditorGUILayout.EnumPopup("충돌 타입 설정", collisionGimmickType);
        GUILayout.Space(5);
        collisionObjectSprite = (Sprite)EditorGUILayout.ObjectField("오브젝트 기본 이미지", collisionObjectSprite, typeof(Sprite), true);
        GUILayout.Space(5);
        if (GUILayout.Button("충돌 타입 생성"))
            gimmickSection.GenerateGimmickCollisionObject(collisionGimmickType, collisionObjectName, collisionObjectSprite);

        // 삭제
        GUILayout.Space(15);
        GUILayout.Label("기믹 오브젝트 삭제", EditorStyles.boldLabel);
        GUILayout.Space(5);
        isRemoveUnlocked = EditorGUILayout.Toggle("삭제 잠금 해제", isRemoveUnlocked);
        removeIndex = EditorGUILayout.IntField("삭제 대상 번호", removeIndex);
        GUILayout.Space(5);
        if (GUILayout.Button("스테이지 섹션 삭제") && isRemoveUnlocked)
            gimmickSection.RemoveGimmickObject(removeIndex);

        GUILayout.Space(20);
    }
}