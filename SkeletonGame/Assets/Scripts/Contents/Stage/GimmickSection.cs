using Steamworks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class GimmickSection : StageSectionBase
{
    /// <summary>
    /// Key : ObjectNum (오브젝트 이름 끝 번호)
    /// </summary>
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

    private void UpdateGimmickComponentDict()
    {
        GimmickComponentDict.Clear();

        Transform[] myChildren = this.GetComponentsInChildren<Transform>();
        foreach(Transform child in myChildren)
        {
            GimmickComponentBase gimmickComponent = child.gameObject.GetComponent<GimmickComponentBase>();
            if(gimmickComponent != null)
            {
                string[] strs = gimmickComponent.gameObject.name.Split(' ');
                int objectNum = int.Parse(strs[strs.Length - 1]);

                GimmickComponentDict.Add(objectNum, gimmickComponent);
            }
        }
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
#endif
}
