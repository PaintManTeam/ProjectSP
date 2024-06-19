using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using UnityEditor;
using System.ComponentModel;
using JetBrains.Annotations;
using System;

/// <summary>
/// 유일성 보장 시켜야 함
/// </summary>
public class StageRoot : InitBase
{
    [Header("에디터 세팅 옵션")]
    // Key : Stage Section Object ID
    Dictionary<int, StageSectionBase> StageSectionDict = new Dictionary<int, StageSectionBase>();
    
    [SerializeField, ReadOnly] StageSectionBase currStageSection = null;
    [SerializeField, ReadOnly] int stageId;
    public int StageId
    {
        get { return stageId; }
        protected set { stageId = value; }
    }

    public BaseMap map { get; protected set; }
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public void StartStage(Player player, int stageSectionId = 1)
    {
        StartSection(player, stageSectionId);
    }

    public void StartSection(Player player, int stageSectionId = 1)
    {
        // 섹션 정보 갱신 ( ID 최대 값 받아옴 )
        SetStageSectionDict();

        // 섹션 확인
        while (StageSectionDict.ContainsKey(stageSectionId) == false)
        {
            // 스테이지 클리어 처리 필요 !!
            return;
        }

        // 시작되는 섹션 받아오고 세팅
        currStageSection = StageSectionDict[stageSectionId];
        player.transform.position = currStageSection.PlayerStartPoint.position;
    }

    private void SetStageSectionDict()
    {
        StageSectionDict.Clear();

        Transform[] myChildren = this.GetComponentsInChildren<Transform>();
        foreach(Transform child in myChildren)
        {
            StageSectionBase stageSection = child.gameObject.GetComponent<StageSectionBase>();

            if(stageSection != null)
            {
                string[] strs = stageSection.gameObject.name.Split(' ');
                int objectNameId = int.Parse(strs[strs.Length - 1]);
                
                if (StageSectionDict.ContainsKey(objectNameId) == false)
                    StageSectionDict.Add(objectNameId, stageSection);
                else
                    Debug.LogWarning($"중복된 번호 : {objectNameId}");
            }
        }
    }

#if UNITY_EDITOR

    public void GenerateStageMap()
    {
        // 맵이 있는 지 확인, 없다면 생성
        map = transform.Find("Map")?.GetComponent<BaseMap>();
        if (map == null)
        {
            string path = $"{PrefabPath.STAGE_PATH}/Map";
            GameObject original = Resources.Load<GameObject>($"Prefabs/{path}");
            GameObject go = Instantiate(original, transform);
            go.name = "Map";
            go.transform.SetAsFirstSibling();
        }
        else
            Debug.Log("이미 맵이 존재합니다.");
    }

    public void SaveStageData()
    {
        Debug.Log("스테이지 데이터 저장 미구현");
    }

    public void LoadStageData()
    {
        Debug.Log("스테이지 데이터 불러오기 미구현");
    }

    public void UpdateStageInfo()
    {
        // 맵이 생성됐는지 확인
        map = transform.Find("Map")?.GetComponent<BaseMap>();
        if (map == null)
        {
            Debug.LogWarning("맵이 없습니다. 맵을 먼저 생성해주세요.");
            return;
        }

        // 스테이지 정보 갱신
        SetStageSectionDict();

        if (StageSectionDict.Count == 0)
            return;

        // 딕셔너리 키 정렬
        int maxObjectId = 1;
        foreach (int num in StageSectionDict.Keys)
            maxObjectId = Mathf.Max(maxObjectId, num);

        // 딕셔너리 번호 순서대로 정렬
        if (maxObjectId > StageSectionDict.Count)
            SortingDictValue(StageSectionDict);

        // 오브젝트 정렬
        for(int i = 1; i <= StageSectionDict.Count; i++)
        {
            StageSectionBase stageSectionBase = StageSectionDict[i];

            if(stageSectionBase != null)
            {
                // 오브젝트 ID 세팅
                string[] strs = stageSectionBase?.gameObject.name.ToString().Split(' ');
                string objectName = "";
                for (int j = 0; j < strs.Length - 1; j++)
                    objectName += $"{strs[j]} ";
                objectName += $"{i}";
                stageSectionBase.name = objectName;

                // 오브젝트 위치 세팅
                stageSectionBase.transform.parent = transform;
                stageSectionBase.transform.SetSiblingIndex(i);
            }
            else
            {
                Debug.LogError($"딕셔너리 세팅 에러 : {i}번째 없음 Dict.Count : {StageSectionDict.Count}");
            }
        }
    }

    private void SortingDictValue<T>(Dictionary<int, T> dict)
    {
        // Dict<Key, Value> -> List[index](Key, Value)
        List<(int, T)> list = new List<(int, T)>();
        foreach(KeyValuePair<int, T> kvp in dict)
            list.Add((kvp.Key, kvp.Value));

        // 정렬
        list.Sort((x, y) => x.Item1 > y.Item1 ? 1 : -1);
        
        // Reset Dict
        dict.Clear();
        for(int i = 0; i < list.Count; i++)
        {
            int key = i + 1;
            T data = list[i].Item2;
            dict.Add(key, data);
        }
    }

    public void AddStageSection(EStageSectionType stageSectionType, int insertIndex)
    {
        UpdateStageInfo();

        // 맵이 생성됐는지 확인
        map = transform.Find("Map")?.GetComponent<BaseMap>();
        if (map == null)
        {
            Debug.LogWarning("맵이 없습니다. 맵을 먼저 생성해주세요.");
            return;
        }

        if (stageSectionType == EStageSectionType.None)
        {
            Debug.LogWarning("스테이지 섹션을 설정해주세요.");
            return;
        }

        if (insertIndex <= 0)
            insertIndex = StageSectionDict.Count + 1;

        // 사이에 삽입되는 경우
        if (StageSectionDict.ContainsKey(insertIndex))
        {
            // 끝부터 삽입위치까지 ID + 1
            for(int currId = StageSectionDict.Count; currId >= insertIndex; currId--)
            {
                // 다음 위치에 추가
                int nextId = currId + 1;
                StageSectionDict.Add(nextId, StageSectionDict[currId]);

                // 오브젝트 ID 변경
                GameObject go = StageSectionDict[currId].gameObject;
                string[] strs = go.name.ToString().Split(' ');
                string objectName = "";
                for (int j = 0; j < strs.Length - 1; j++)
                    objectName += $"{strs[j]} ";
                objectName += $"{nextId}";
                go.name = objectName;

                // 삭제
                StageSectionDict.Remove(currId);
            }
        }

        StageSectionBase addSection = GenerateStageSection(stageSectionType, insertIndex);

        StageSectionDict.Add(insertIndex, addSection);
        UpdateStageInfo();
    }

    private StageSectionBase GenerateStageSection(EStageSectionType stageSectionType, int insertIndex = 0)
    {
        if (insertIndex <= 0)
            insertIndex = StageSectionDict.Count + 1;

        GameObject go = Util.InstantiateObject(transform);

        switch (stageSectionType)
        {
            case EStageSectionType.GimmickSection:
                go.AddComponent<GimmickSection>();
                break;
            case EStageSectionType.CinematicSection:
                go.AddComponent<CinematicSection>();
                break;
            default:
                Debug.LogError($"새로운 타입 추가 필요 : {stageSectionType}");
                return null;
        }

        go.name = stageSectionType.ToString() + $" {insertIndex}";

        Debug.Log("스테이지 섹션 생성 완료");

        return go.GetComponent<StageSectionBase>();
    }
     
    public void RemoveStageSection(int removeIndex)
    {
        UpdateStageInfo();

        if (removeIndex <= 0)
            removeIndex = StageSectionDict.Count;

        if (StageSectionDict.ContainsKey(removeIndex) == false)
        {
            Debug.LogWarning("삭제할 대상이 없습니다.");
            return;
        }

        DestroyImmediate(StageSectionDict[removeIndex].gameObject);

        UpdateStageInfo();
    }

#endif
}
