using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class GimmickSection : StageSectionBase
{
    [SerializeField, ReadOnly] List<int> GimmickInstanceIDList = new List<int>();
    [SerializeField, ReadOnly] List<IGimmickComponent> GimmickComponentList = new List<IGimmickComponent>();

    // Key : InstanceID
    Dictionary<int, GimmickComponentBase> GimmickComponentDict = new Dictionary<int, GimmickComponentBase>();

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        

        return true;
    }

    protected virtual void Reset()
    {
        
    }

#if UNITY_EDITOR

    public void SaveSectionData()
    {
        Debug.Log("섹션 데이터 저장 미구현");
    }

    public void LoadSectionData()
    {
        Debug.Log("섹션 데이터 불러오기 미구현");
    }

    /// <summary>
    /// 1번부터 비어있는 오브젝트 번호를 반환함
    /// </summary>
    public int UpdateSectionInfo()
    {

        Transform[] myChildren = this.GetComponents<Transform>();
        int maxIndex = -1;
        foreach(Transform child in myChildren)
        {
            GimmickComponentBase gimmickComponent = child.gameObject.GetComponent<GimmickComponentBase>();
            if (gimmickComponent != null)
                GimmickComponentDict.Add(gimmickComponent.GetInstanceID(), gimmickComponent);
            
        }

        if (GimmickComponentDict.Count == 0 || maxIndex == -1)
            return -1;

        

        return 1;
    }

    private int GetNextObjectNum()
    {
        // 초기화
        GimmickComponentDict.Clear();
        GimmickInstanceIDList.Clear();

        // GimmickComponentDict 갱신 (마지막 번호, 비어있는 번호 체크)
        Transform[] myChildren = this.GetComponentsInChildren<Transform>();
        bool[] boolArray = new bool[myChildren.Length + 1];
        int maxNum = 1;

        foreach (Transform child in myChildren)
        {
            GimmickComponentBase gimmickComponent = child.gameObject.GetComponent<GimmickComponentBase>();
            if (gimmickComponent != null)
            {
                GimmickComponentDict.Add(gimmickComponent.GetInstanceID(), gimmickComponent);

                string[] strs = gimmickComponent.gameObject.name.Split(' ');
                int objectNum = int.Parse(strs[strs.Length - 1]);
                maxNum = Mathf.Max(maxNum, objectNum);
                boolArray[objectNum] = true;
            }
        }

        maxNum++; // NextNum

        if (GimmickComponentDict.Count == 0)
        {
            Debug.Log("1");
            return 1;
        }

        // 비어있는 번호 체크
        for (int i = 1; i < boolArray.Length; i++)
            if (boolArray[i] == false)
                return i;

        Debug.Log("2");

        return maxNum;
    }

    public GimmickInteractionComponent GenerateGimmickInteractionObject(EGimmickInteractionObjectType gimmickObjectType)
    {
        if (gimmickObjectType == EGimmickInteractionObjectType.None)
        {
            Debug.LogWarning("생성할 상호작용 타입을 설정해주세요.");
            return null;
        }

        GameObject go = Util.InstantiateObject(transform);

        switch (gimmickObjectType)
        {
            case EGimmickInteractionObjectType.EndMotion:
                // 테스트용 임시?
                go.AddComponent<GimmickComponentBase>();
                break;
            case EGimmickInteractionObjectType.Dialogue:
                go.AddComponent<DialogueComponent>();
                break;
            case EGimmickInteractionObjectType.Portal:
                go.AddComponent<PortalComponent>();
                break;
            default:
                Debug.LogError($"새로운 타입 추가 필요 : {gimmickObjectType}");
                return null;
        }

        go.name = gimmickObjectType.ToString() + $" 0";
        int objectNum = GetNextObjectNum();
        go.name = gimmickObjectType.ToString() + $" {objectNum}";

        return go.GetComponent<GimmickInteractionComponent>();
    }

    public GimmickCollisionComponent AddGimmickCollisionObject(EGimmickCollisionObjectType gimmickObjectType)
    {
        if(gimmickObjectType == EGimmickCollisionObjectType.None)
        {
            Debug.LogWarning("생성할 충돌 타입을 설정해주세요.");
            return null;
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
                return null;
        }

        int objectNum = GetNextObjectNum();
        go.name = gimmickObjectType.ToString() + $" {objectNum}";

        return go.GetComponent<GimmickCollisionComponent>();
    }

#endif
}
