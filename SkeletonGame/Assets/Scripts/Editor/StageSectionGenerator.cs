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
    int stageSectionID;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StageRoot stageRoot = (StageRoot)target;

        GUILayout.Label("Map Setting Menu", EditorStyles.boldLabel);
        if (GUILayout.Button("테스트 맵 생성"))
        {
            stageRoot.GenerateStageMap();
        }

        GUILayout.Label("Stage Section Generate Menu", EditorStyles.boldLabel);
        stageSectionType = (EStageSectionType)EditorGUILayout.EnumPopup("스테이지 섹션 타입", stageSectionType);

        if (GUILayout.Button("스테이지 색션 생성"))
        {
            stageRoot.GenerateStageSection(stageSectionType);
        }

        if (GUILayout.Button("스테이지 색션 갱신"))
        {
            stageRoot.UpdateStageInfo();
        }
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