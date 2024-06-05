using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class StageRoot : InitBase
{
    [SerializeField] // 확인용
    List<int> InstanceIDList = new List<int>();

    [SerializeField] BaseMap map;
    [SerializeField] List<StageSectionBase> StageSectionList = new List<StageSectionBase>();

    

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        

        return true;
    }

#if UNITY_EDITOR

    public void GenerateStageMap()
    {
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

    public bool UpdateStageInfo()
    {
        map = transform.Find("Map")?.GetComponent<BaseMap>();

        if (map == null)
        {
            Debug.LogWarning("맵을 먼저 생성해주세요.");
            return false;
        }

        StageSectionList.Clear();

        Transform[] myChildren = this.GetComponentsInChildren<Transform>();

        foreach (Transform child in myChildren)
        {
            StageSectionBase stageSection = child.gameObject.GetComponent<StageSectionBase>();

            if (stageSection != null)
                StageSectionList.Add(stageSection);
        }

        for (int i = 0; i < StageSectionList.Count; i++)
        {
            string[] strs = StageSectionList[i].ToString().Split(' ');
            StageSectionList[i].name = strs[0] + $" {i + 1}";
            StageSectionList[i].transform.parent = transform;
            StageSectionList[i].transform.SetSiblingIndex(i + 1);
            
            Debug.Log($"{i}번째 : {StageSectionList[i].GetInstanceID()}");
        }

        return true;
    }

    public void GenerateStageSection(EStageSectionType stageSectionType)
    {
        if(stageSectionType == EStageSectionType.None)
        {
            Debug.LogWarning("스테이지 섹션을 설정해주세요.");
            return;
        }

        bool b = UpdateStageInfo();
        if (b == false)
            return;

        GameObject tempObject = new GameObject();
        GameObject go = Instantiate(tempObject, transform);
        DestroyImmediate(tempObject);

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
                break;
        }

        StageSectionList.Add(go.GetComponent<StageSectionBase>());
        go.name = $"{stageSectionType} {StageSectionList.Count}";
    }
#endif
}
