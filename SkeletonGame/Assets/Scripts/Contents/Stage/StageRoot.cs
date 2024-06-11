using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using UnityEditor;
using System.ComponentModel;

public class StageRoot : InitBase
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

#if UNITY_EDITOR

    [Header("에디터 세팅 옵션")]
    [SerializeField, ReadOnly] List<int> SectionInstanceIDList = new List<int>();
    [SerializeField] BaseMap map;
    
    /// <summary>
    /// Key : Object InstacneID
    /// </summary>
    Dictionary<int, StageSectionBase> StageSectionDict = new Dictionary<int, StageSectionBase>();

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

        // 초기화
        StageSectionDict.Clear();
        SectionInstanceIDList.Clear();

        // StageSectionDict 갱신
        Transform[] myChildren = this.GetComponentsInChildren<Transform>();
        foreach (Transform child in myChildren)
        {
            StageSectionBase stageSection = child.gameObject.GetComponent<StageSectionBase>();
            if (stageSection != null)
                StageSectionDict.Add(stageSection.GetInstanceID(), stageSection);
        }

        if (StageSectionDict.Count == 0)
            return;

        // 번호 순서대로 정렬
        List<(int, int)> tempList = new List<(int, int)>(); // (번호, 인스턴스ID)
        foreach (StageSectionBase stageSection in StageSectionDict.Values)
        {
            string[] strs = stageSection.gameObject.name.ToString().Split(' ');
            int num = int.Parse(strs[strs.Length - 1]);
            tempList.Add((num, stageSection.GetInstanceID())); 
        }
        tempList.Sort((x, y) => x.Item1 > y.Item1 ? 1 : -1);

        // SectionInstanceIDList 세팅
        for (int i = 0; i < tempList.Count; i++)
            SectionInstanceIDList.Add(tempList[i].Item2);

        // 오브젝트 정렬
        for(int i = 0; i < SectionInstanceIDList.Count; i++)
        {
            StageSectionBase stageSectionBase = StageSectionDict[SectionInstanceIDList[i]];

            // 오브젝트 번호 세팅
            string[] strs = stageSectionBase?.gameObject.name.ToString().Split(' ');
            string objectName = "";
            for(int j = 0; j < strs.Length - 1; j++)
                objectName += $"{strs[j]} ";
            objectName += $"{i + 1}";
            stageSectionBase.name = objectName;

            // 오브젝트 위치 세팅
            stageSectionBase.transform.parent = transform;
            stageSectionBase.transform.SetSiblingIndex(i + 1);
        }
    }

    public void AddStageSection(EStageSectionType stageSectionType, int insertIndex)
    {
        UpdateStageInfo();

        StageSectionBase addSection = GenerateStageSection(stageSectionType);
        
        if (addSection == null)
            return;

        if(insertIndex <= 0)
        {
            // 맨 끝에 추가
            addSection.gameObject.name += $" {SectionInstanceIDList.Count + 1}";
        }
        else
        {
            // 삽입
            addSection.gameObject.name += $" {insertIndex}";

            for (int i = insertIndex; i < SectionInstanceIDList.Count; i++)
            {
                StageSectionBase stageSectionBase = StageSectionDict[SectionInstanceIDList[i]];

                string[] strs = stageSectionBase.gameObject.name.Split(' ');
                string objectName = "";
                for (int j = 0; j < strs.Length - 1; j++)
                    objectName += strs[j];
                objectName += $" {strs[strs.Length - 1] + 1}";
                stageSectionBase.gameObject.name = objectName;
            }
        }

        UpdateStageInfo();
    }

    private StageSectionBase GenerateStageSection(EStageSectionType stageSectionType, int insertIndex = -1)
    {
        // 맵이 생성됐는지 확인
        map = transform.Find("Map")?.GetComponent<BaseMap>();
        if (map == null)
        {
            Debug.LogWarning("맵이 없습니다. 맵을 먼저 생성해주세요.");
            return null;
        }

        if (stageSectionType == EStageSectionType.None)
        {
            Debug.LogWarning("스테이지 섹션을 설정해주세요.");
            return null;
        }

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

        go.name = stageSectionType.ToString();

        Debug.Log("스테이지 섹션 생성 완료");

        return go.GetComponent<StageSectionBase>();
    }
     
    public void RemoveStageSection(int removeIndex)
    {
        UpdateStageInfo();
                                                                                                 
        if (SectionInstanceIDList.Count < removeIndex || SectionInstanceIDList.Count == 0)
        {
            Debug.LogWarning("삭제할 대상이 없습니다.");
            return;
        }

        if (removeIndex <= 0)
        {
            DestroyImmediate(StageSectionDict[SectionInstanceIDList[SectionInstanceIDList.Count - 1]].gameObject);
        }
        else
        {
            DestroyImmediate(StageSectionDict[SectionInstanceIDList[removeIndex - 1]].gameObject);
        }

        UpdateStageInfo();
    }

#endif
}
