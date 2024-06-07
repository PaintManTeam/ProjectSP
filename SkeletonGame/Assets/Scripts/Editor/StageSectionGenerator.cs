using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Define;

[CustomEditor(typeof(StageRoot))]
public class StageSectionGenerator : Editor
{
    EStageSectionType stageSectionType;

    bool isSaveUnlocked;
    bool isLoadUnlocked;
    bool isRemoveUnlocked;

    int insertIndex;
    int removeIndex;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StageRoot stageRoot = (StageRoot)target;

        // 맵 관련
        GUILayout.Space(10);
        GUILayout.Label("- 배경 맵 -", EditorStyles.boldLabel);
        GUILayout.Space(5);
        if (GUILayout.Button("테스트 맵 생성"))
            stageRoot.GenerateStageMap();

        GUILayout.Space(15);
        GUILayout.Label("- 스테이지 -", EditorStyles.boldLabel);
        
        // 갱신
        GUILayout.Space(5);
        if (GUILayout.Button("스테이지 데이터 갱신"))
            stageRoot.UpdateStageInfo();

        // 데이터 저장
        GUILayout.Space(5);
        isSaveUnlocked = EditorGUILayout.Toggle("저장 잠금 해제", isSaveUnlocked);
        GUILayout.Space(5);
        if (GUILayout.Button("스테이지 데이터 저장") && isSaveUnlocked)
            stageRoot.SaveStageData();
        
        // 데이터 불러오기
        GUILayout.Space(5);
        isLoadUnlocked = EditorGUILayout.Toggle("불러오기 잠금 해제", isLoadUnlocked);
        GUILayout.Space(5);
        if (GUILayout.Button("스테이지 데이터 불러오기") && isLoadUnlocked)
            stageRoot.LoadStageData();
        
        // 섹션 추가
        GUILayout.Space(15);
        GUILayout.Label("스테이지 섹션 추가", EditorStyles.boldLabel);
        GUILayout.Space(5);
        stageSectionType = (EStageSectionType)EditorGUILayout.EnumPopup("스테이지 섹션 타입", stageSectionType);
        insertIndex = EditorGUILayout.IntField("위치 (0이면 맨끝)", insertIndex);
        GUILayout.Space(5);
        if (GUILayout.Button("스테이지 색션 추가"))
            stageRoot.AddStageSection(stageSectionType, insertIndex);

        // 섹션 삭제
        GUILayout.Space(15);
        GUILayout.Label("스테이지 섹션 삭제", EditorStyles.boldLabel);
        GUILayout.Space(5);
        isRemoveUnlocked = EditorGUILayout.Toggle("삭제 잠금 해제", isRemoveUnlocked);
        removeIndex = EditorGUILayout.IntField("삭제대상 (0이면 맨끝)", removeIndex);
        GUILayout.Space(5);
        if (GUILayout.Button("스테이지 섹션 삭제") && isRemoveUnlocked)
            stageRoot.RemoveStageSection(removeIndex);

        GUILayout.Space(20);
    }
}