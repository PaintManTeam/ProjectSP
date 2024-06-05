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
    EStageSectionType insertStageSectionType;

    bool isSaveUnlockedStatus;
    bool isLoadUnlockedStatus;
    bool isRemoveUnlockedStatus;

    int insertIndex;
    int removeIndex;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StageRoot stageRoot = (StageRoot)target;

        // 맵 관련
        GUILayout.Space(10);
        GUILayout.Label("맵 관련 메뉴", EditorStyles.boldLabel);
        GUILayout.Space(5);
        if (GUILayout.Button("테스트 맵 생성"))
            stageRoot.GenerateStageMap();

        GUILayout.Space(15);
        GUILayout.Label("스테이지 관련 메뉴", EditorStyles.boldLabel);
        
        // 갱신
        GUILayout.Space(5);
        if (GUILayout.Button("스테이지 색션 갱신"))
            stageRoot.UpdateStageInfo();

        // 데이터 저장
        GUILayout.Space(5);
        isSaveUnlockedStatus = EditorGUILayout.Toggle("저장 잠금 해제", isSaveUnlockedStatus);
        GUILayout.Space(5);
        if (GUILayout.Button("스테이지 색션 저장") && isSaveUnlockedStatus)
            stageRoot.SaveStageSectionDatas();
        
        // 데이터 불러오기
        GUILayout.Space(5);
        isLoadUnlockedStatus = EditorGUILayout.Toggle("불러오기 잠금 해제", isLoadUnlockedStatus);
        GUILayout.Space(5);
        if (GUILayout.Button("스테이지 색션 불러오기") && isLoadUnlockedStatus)
            stageRoot.LoadStageSectionDatas();
        
        // 추가
        GUILayout.Space(15);
        GUILayout.Label("스테이지 추가", EditorStyles.label);
        GUILayout.Space(5);
        stageSectionType = (EStageSectionType)EditorGUILayout.EnumPopup("스테이지 섹션 타입", stageSectionType);
        insertIndex = EditorGUILayout.IntField("위치 (0이면 맨끝)", insertIndex);
        GUILayout.Space(5);
        if (GUILayout.Button("스테이지 색션 추가"))
            stageRoot.AddStageSection(stageSectionType, insertIndex);

        // 삭제
        GUILayout.Space(15);
        GUILayout.Label("스테이지 삭제", EditorStyles.label);
        GUILayout.Space(5);
        isRemoveUnlockedStatus = EditorGUILayout.Toggle("삭제 잠금 해제", isRemoveUnlockedStatus);
        removeIndex = EditorGUILayout.IntField("삭제대상 (0이면 맨끝)", removeIndex);
        GUILayout.Space(5);
        if (GUILayout.Button("스테이지 섹션 삭제") && isRemoveUnlockedStatus)
            stageRoot.RemoveStageSection(removeIndex);

        GUILayout.Space(20);
    }
}
namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute), true)]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        // Necessary since some properties tend to collapse smaller than their content
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        // Draw a disabled property field
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = !Application.isPlaying && ((ReadOnlyAttribute)attribute).runtimeOnly;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : PropertyAttribute
{
    public readonly bool runtimeOnly;

    public ReadOnlyAttribute(bool runtimeOnly = false)
    {
        this.runtimeOnly = runtimeOnly;
    }
}