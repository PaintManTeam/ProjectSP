using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class StageRoot : InitBase
{
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
        string path = $"{PrefabPath.STAGE_PATH}/Map";
        GameObject original = Resources.Load<GameObject>($"Prefabs/{path}");
        GameObject go = Instantiate(original, transform);
        go.name = "Map";
        go.transform.SetAsFirstSibling();
    }

    public bool UpdateStageInfo()
    {
        map = transform.Find("Map")?.GetComponent<BaseMap>();

        if (map == null)
        {
            Debug.LogError("맵을 먼저 생성해주세요.");
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

        foreach(StageSectionBase child in StageSectionList)
        {
            // 이름 정리?
        }

        return true;
    }

    public void GenerateStageSection(EStageSectionType stageSectionType)
    {
        if(stageSectionType == EStageSectionType.None)
        {
            Debug.LogError("스테이지 섹션을 설정해주세요.");
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
            case EStageSectionType.GimmickPuzzle:
                go.AddComponent<GimmickSection>();
                break;
            case EStageSectionType.Cinematic:
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
