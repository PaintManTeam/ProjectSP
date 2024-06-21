using Data;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Define;

public class GimmickSection : StageSectionBase
{
    /// <summary>
    /// Key : ObjectNum (오브젝트 이름 끝 번호)
    /// </summary>
    Dictionary<int, GimmickComponentBase> GimmickComponentDict = new Dictionary<int, GimmickComponentBase>();
    protected override void Reset()
    {
        base.Reset();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        UpdateGimmickComponentDict();

        foreach(GimmickComponentBase gimmickComponent in GimmickComponentDict.Values)
        {
            if(gimmickComponent is GimmickInteractionComponent gimmickInteractionComponent)
            {
                gimmickInteractionComponent.SetInfo(OnInteractionEvent);
            }

            EditGimmickComponentInfo editGimmickComponentInfo = gimmickComponent.gameObject.GetComponent<EditGimmickComponentInfo>();
            if (editGimmickComponentInfo != null)
                Destroy(editGimmickComponentInfo);
        }
        
        return true;
    }

    public void OnInteractionEvent(int gimmickObjectId)
    {
        foreach (GimmickComponentBase gimmickComponent in GimmickComponentDict.Values)
        {
            gimmickComponent.CheckListOfConditionToRemove();
        }
    }

    public void UpdateGimmickComponentDict()
    {
        GimmickComponentDict.Clear();

        Transform[] myChildren = this.GetComponentsInChildren<Transform>();
        foreach (Transform child in myChildren)
        {
            GimmickComponentBase gimmickComponent = child.gameObject.GetComponent<GimmickComponentBase>();
            if (gimmickComponent != null)
            {
                string[] strs = gimmickComponent.gameObject.name.Split(' ');
                int objectNum = int.Parse(strs[strs.Length - 1]);

                GimmickComponentDict.Add(objectNum, gimmickComponent);
            }
#if UNITY_EDITOR
            EditGimmickComponentInfo editGimmickComponentInfo = child.gameObject.GetComponent<EditGimmickComponentInfo>();
            if(editGimmickComponentInfo != null)
            {
                editGimmickComponentInfo.SetInfo(AddActiveObjectCondition, RemoveActiveObjectCondition,
                    AddGimmickReadyConditionList, RemoveGimmickReadyConditionList);
            }
#endif
        }
    }

#if UNITY_EDITOR

    public void SaveSectionData()
    {
        UpdateGimmickComponentDict();

        string[] strs = transform.parent.name.Split(' ');
        int stageId = int.Parse(strs[strs.Length - 1]);

        // 경로 미 존재 시 생성
        string path = Application.dataPath + DataPath.STAGEDATA_PATH + $"/Stage {stageId}";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(path + "/GimmickSection");
            Directory.CreateDirectory(path + "/CinematicSection");
        }

        // 기믹 섹션 데이터 세이브
        foreach (GimmickComponentBase gimmickComponentBase in GimmickComponentDict.Values)
        {
            string savePath = path + $"/GimmickSection/GimmickComponentData {gimmickComponentBase.GimmickObjectId}.json";
            List<int> intActiveObjectConditionList = gimmickComponentBase.GetIntActiveObjectConditionList();
            List<int> intGimmickReadyConditionList = gimmickComponentBase.GetIntGimmickReadyConditionList();

            // 기존 저장된 데이터가 있는지 확인해 삭제
            if (File.Exists(savePath))
                File.Delete(savePath);

            // 저장할 데이터가 없는 경우 
            if (intActiveObjectConditionList.Count == 0 && intActiveObjectConditionList.Count == 0)
                continue;

            GimmickComponentData gimmickComponentData = new GimmickComponentData(
            gimmickComponentBase.GimmickObjectId, intActiveObjectConditionList, intGimmickReadyConditionList);
            
            string jsonData = JsonUtility.ToJson(gimmickComponentData);
            File.WriteAllText(savePath, jsonData);
        }

        // 시네마틱 섹션 데이터 세이브
        Debug.Log("시네마틱 섹션 데이터 세이브 미구현");

        Debug.Log("섹션 데이터 세이브 요청 완료");
    }

    public void LoadSectionData()
    {
        UpdateGimmickComponentDict();

        string[] strs = transform.parent.name.Split(' ');
        int stageId = int.Parse(strs[strs.Length - 1]);

        string path = Application.dataPath + DataPath.STAGEDATA_PATH + $"/Stage {stageId}";

        // 기믹 섹션 데이터 로드
        foreach (GimmickComponentBase gimmickComponentBase in GimmickComponentDict.Values)
        {
            string loadPath = path + $"/GimmickSection/GimmickComponentData {gimmickComponentBase.GimmickObjectId}.json";

            // 불러올 데이터가 없는 경우
            if (File.Exists(loadPath) == false)
                continue;

            string jsonData = File.ReadAllText(loadPath);

            GimmickComponentData gimmickComponentData = JsonUtility.FromJson<GimmickComponentData>(jsonData);

            List<GimmickComponentBase> activeObjectConditionList = new();
            foreach (int id in gimmickComponentData.ActiveObjectConditionList)
                activeObjectConditionList.Add(GimmickComponentDict[id]);

            List<GimmickComponentBase> gimmickReadyConditionList = new();
            foreach (int id in gimmickComponentData.GimmickReadyConditionList)
                gimmickReadyConditionList.Add(GimmickComponentDict[id]);

            gimmickComponentBase.SetGimmickComponentData(
                activeObjectConditionList: activeObjectConditionList,
                gimmickReadyConditionList: gimmickReadyConditionList);
        }

        // 시네마틱 섹션 데이터 로드
        Debug.Log("시네마틱 섹션 데이터 로드 미구현");

        Debug.Log("섹션 데이터 로드 완료");
    }

    /// <summary>
    /// 에러가 있을 경우 true ( + 오브젝트 정보 갱신 )
    /// </summary>
    private bool CheckForErrors()
    {
        // 중복된 번호가 존재
        if (GetNextObjectNum() == -1)
        {
            // 로그는 메서드 내에서 남김
            return true;
        }

        return false;
    }

    private int GetNextObjectNum()
    {
        UpdateGimmickComponentDict();

        if (GimmickComponentDict.Count == 0)
            return 1;

        int maxNum = 0;
        foreach (int objectNum in GimmickComponentDict.Keys)
            maxNum = Mathf.Max(objectNum, maxNum);

        bool[] boolArray = new bool[maxNum + 1];
        foreach (var objectNum in GimmickComponentDict.Keys)
        {
            if (boolArray[objectNum])
            {
                Debug.LogWarning($"아이디 중복 : {objectNum}");
                return -1;
            }

            boolArray[objectNum] = true;
        }
        maxNum++; // NextNum

        // 비어있는 번호 체크
        for (int i = 1; i < boolArray.Length; i++)
            if (boolArray[i] == false)
                return i;

        return maxNum;
    }

    public void GenerateGimmickInteractionObject(
        EGimmickInteractionObjectType gimmickObjectType, string objectName, Sprite objectSprite)
    {
        if (CheckForErrors())
            return;

        if (gimmickObjectType == EGimmickInteractionObjectType.None)
        {
            Debug.LogWarning("생성할 상호작용 타입을 설정해주세요.");
            return;
        }

        GameObject go = Util.InstantiateObject(transform);
        go.AddComponent<EditGimmickComponentInfo>();

        switch (gimmickObjectType)
        {
            case EGimmickInteractionObjectType.Dialogue:
                go.AddComponent<DialogueComponent>();
                break;
            case EGimmickInteractionObjectType.Portal:
                go.AddComponent<PortalComponent>();
                break;
            case EGimmickInteractionObjectType.Destroy:
                go.AddComponent<DestroyInteractionComponent>();
                break;
            default:
                Debug.LogError($"새로운 타입 추가 필요 : {gimmickObjectType}");
                return;
        }

        GimmickComponentBase gimmickComponent = go.GetComponent<GimmickComponentBase>();

        if (objectSprite != null)
            gimmickComponent?.SetSpriteRenderer(objectSprite);

        if (string.IsNullOrEmpty(objectName))
            objectName = gimmickObjectType.ToString();

        go.name = objectName + $" 0";
        int objectNum = GetNextObjectNum();
        go.name = objectName + $" {objectNum}";

        UpdateGimmickComponentDict();
        return;
    }

    public void GenerateGimmickCollisionObject(
        EGimmickCollisionObjectType gimmickObjectType, string objectName, Sprite objectSprite)
    {
        if (CheckForErrors())
            return;

        if(gimmickObjectType == EGimmickCollisionObjectType.None)
        {
            Debug.LogWarning("생성할 충돌 타입을 설정해주세요.");
            return;
        }

        GameObject go = Util.InstantiateObject(transform);

        switch(gimmickObjectType)
        {
            case EGimmickCollisionObjectType.TestCollsion:
                // 임시처리
                go.GetComponent<GimmickCollisionComponent>();
                break;
            default:
                Debug.LogError($"새로운 타입 추가 필요 : {gimmickObjectType}");
                return;
        }

        GimmickComponentBase gimmickComponent = go.GetComponent<GimmickComponentBase>();

        if (objectSprite != null)
            gimmickComponent?.SetSpriteRenderer(objectSprite);

        if (string.IsNullOrEmpty(objectName))
            objectName = gimmickObjectType.ToString();

        go.name = objectName + $" 0";
        int objectNum = GetNextObjectNum();
        go.name = objectName + $" {objectNum}";

        UpdateGimmickComponentDict();
        return;
    }

    public void RemoveGimmickObject(int removeIndex)
    {
        if (CheckForErrors())
            return;

        UpdateGimmickComponentDict();
        
        // 삭제할 대상이 있는지 서치
        if(GimmickComponentDict.ContainsKey(removeIndex))
        {
            DestroyImmediate(GimmickComponentDict[removeIndex].gameObject);
        }
        else
        {
            Debug.LogWarning("삭제 대상인 오브젝트가 없습니다.");
            return;
        }
    }

    private bool IsCheckContainsKey(int requestObjectId, int receiveObjectId)
    {
        if (requestObjectId == receiveObjectId)
        {
            Debug.LogWarning("자기 자신은 등록/삭제 할 수 없습니다.");
            return false;
        }

        UpdateGimmickComponentDict();

        if (GimmickComponentDict.ContainsKey(requestObjectId) == false)
        {
            Debug.LogWarning($"{requestObjectId}번 키가 없습니다.");
            return false;
        }
        if (GimmickComponentDict.ContainsKey(receiveObjectId) == false)
        {
            Debug.LogWarning($"{receiveObjectId}번 키가 없습니다.");
            return false;
        }

        return true;
    }

    public void AddActiveObjectCondition(int requestObjectId, int receiveObjectId)
    {
        if (IsCheckContainsKey(requestObjectId, receiveObjectId) == false)
            return;

        GimmickComponentBase receiveGimmickComponent = GimmickComponentDict[receiveObjectId];
        GimmickComponentDict[requestObjectId].AddActiveObjectCondition(receiveGimmickComponent);
    }

    public void RemoveActiveObjectCondition(int requestObjectId, int receiveObjectId)
    {
        if (IsCheckContainsKey(requestObjectId, receiveObjectId) == false)
            return;

        GimmickComponentBase receiveGimmickComponent = GimmickComponentDict[receiveObjectId];
        GimmickComponentDict[requestObjectId].RemoveActiveObjectCondition(receiveGimmickComponent);
    }

    public void AddGimmickReadyConditionList(int requestObjectId, int receiveObjectId)
    {
        if (IsCheckContainsKey(requestObjectId, receiveObjectId) == false)
            return;

        GimmickComponentBase receiveGimmickComponent = GimmickComponentDict[receiveObjectId];
        GimmickComponentDict[requestObjectId].AddGimmickReadyConditionList(receiveGimmickComponent);
    }

    public void RemoveGimmickReadyConditionList(int requestObjectId, int receiveObjectId)
    {
        if (IsCheckContainsKey(requestObjectId, receiveObjectId) == false)
            return;

        GimmickComponentBase receiveGimmickComponent = GimmickComponentDict[receiveObjectId];
        GimmickComponentDict[requestObjectId].RemoveGimmickReadyConditionList(receiveGimmickComponent);
    }
#endif
}
