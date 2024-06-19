using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditGimmickComponentInfo))]
public class GimmickComponentSetting : Editor
{
    // 오브젝트 활성화 조건
    bool isActiveConditionListRemoveUnlock = false;
    int addActiveConditionGimmickObjectId = 0;
    int removeActiveConditionGimmickObjectId = 0;

    // 기믹 준비 상태 조건
    bool isReadyConditionListRemoveUnlocked = false;
    int addReadyConditionGimmickObjectId = 0;
    int removeReadyConditionGimmickObjectId = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GimmickComponentBase gimmickComponentBase = target.GetComponent<GimmickComponentBase>();
        EditGimmickComponentInfo editGimmickComponentInfo = (EditGimmickComponentInfo)target;

        // 오브젝트 활성화 조건 추가
        GUILayout.Space(10);
        GUILayout.Label("오브젝트 활성화 조건 추가", EditorStyles.boldLabel);
        GUILayout.Space(5);
        addActiveConditionGimmickObjectId = EditorGUILayout.IntField("추가할 Obj Id : ", addActiveConditionGimmickObjectId);
        GUILayout.Space(5);
        if (GUILayout.Button("오브젝트 활성화 조건 추가"))
            editGimmickComponentInfo.AddActiveObjectCondition(gimmickComponentBase.GimmickObjectId, addActiveConditionGimmickObjectId);

        // 오브젝트 활성화 조건 삭제
        GUILayout.Space(10);
        GUILayout.Label("오브젝트 활성화 조건 삭제", EditorStyles.boldLabel);
        GUILayout.Space(5);
        isActiveConditionListRemoveUnlock = EditorGUILayout.Toggle("삭제 잠금 해제", isActiveConditionListRemoveUnlock);
        removeActiveConditionGimmickObjectId = EditorGUILayout.IntField("삭제 대상 번호", removeActiveConditionGimmickObjectId);
        GUILayout.Space(5);
        if(GUILayout.Button("오브젝트 활성화 조건 삭제") && isActiveConditionListRemoveUnlock)
            editGimmickComponentInfo.RemoveActiveObjectCondition(gimmickComponentBase.GimmickObjectId, removeActiveConditionGimmickObjectId);

        // 기믹 준비 조건 추가
        GUILayout.Space(10);
        GUILayout.Label("기믹 준비 조건 추가", EditorStyles.boldLabel);
        GUILayout.Space(5);
        addReadyConditionGimmickObjectId = EditorGUILayout.IntField("추가할 Obj Id : ", addReadyConditionGimmickObjectId);
        GUILayout.Space(5);
        if (GUILayout.Button("기믹 준비 조건 추가"))
            editGimmickComponentInfo.AddGimmickReadyConditionList(gimmickComponentBase.GimmickObjectId, addReadyConditionGimmickObjectId);

        // 기믹 준비 조건 삭제
        GUILayout.Space(10);
        GUILayout.Label("기믹 준비 조건 삭제", EditorStyles.boldLabel);
        GUILayout.Space(5);
        isReadyConditionListRemoveUnlocked = EditorGUILayout.Toggle("삭제 잠금 해제", isReadyConditionListRemoveUnlocked);
        removeReadyConditionGimmickObjectId = EditorGUILayout.IntField("삭제 대상 번호", removeReadyConditionGimmickObjectId);
        GUILayout.Space(5);
        if (GUILayout.Button("기믹 준비 조건 삭제") && isReadyConditionListRemoveUnlocked)
            editGimmickComponentInfo.RemoveGimmickReadyConditionList(gimmickComponentBase.GimmickObjectId, removeReadyConditionGimmickObjectId);


        GUILayout.Space(20);
    }
}
