using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.Mathematics;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public interface IGimmickComponent
{
    public EGimmickObjectState GimmickState { get; }
    public EGimmickType GimmickType { get; }
}

public abstract class GimmickComponentBase : InitBase, IGimmickComponent
{
    [SerializeField, ReadOnly] int gimmickObjectId;
    public int GimmickObjectId
    {
        get
        {
            if (gimmickObjectId <= 0)
            {
                string[] strs = gameObject.name.Split(' ');
                gimmickObjectId = int.Parse(strs[strs.Length - 1]);
            }

            return gimmickObjectId;
        }
        private set { gimmickObjectId = value; }
    }

    [Header("오브젝트 활성화 조건")]
    [SerializeField, ReadOnly] private List<GimmickComponentBase> activeObjectConditionList;
    public List<GimmickComponentBase> ActiveObjectConditionList
    {
        get
        {
            activeObjectConditionList ??= new();
            return activeObjectConditionList;
        }
        protected set
        {
            activeObjectConditionList = value;
        }
    }

    [Header("기믹 준비 조건")]
    [SerializeField, ReadOnly] private List<GimmickComponentBase> gimmickReadyConditionList;
    public List<GimmickComponentBase> GimmickReadyConditionList
    {
        get
        {
            gimmickReadyConditionList ??= new();
            return gimmickReadyConditionList;
        }
        protected set
        {
            gimmickReadyConditionList = value;
        }
    }


    public EGimmickObjectState GimmickState { get; protected set; }
    public EGimmickType GimmickType { get; protected set; }

    public SpriteRenderer Sprite { get; protected set; }
    public Rigidbody2D Rigidbody { get; protected set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Rigidbody = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();

        GimmickType = EGimmickType.Interaction;

        return true;
    }

    private void SetGimmickState()
    {
        // 오브젝트 활성화
        this.gameObject.SetActive(ActiveObjectConditionList.Count == 0);

        // 오브젝트 레디 조건
        GimmickState = (GimmickReadyConditionList.Count == 0) ?
            EGimmickObjectState.Ready : EGimmickObjectState.StandBy;
    }

    public void CheckListOfConditionToRemove()
    {
        if (GimmickState != EGimmickObjectState.StandBy)
            return;

        CheckListOfActiveCondition();
        CheckListOfReadyCondition();

        SetGimmickState();
    }

    private void CheckListOfActiveCondition()
    {
        if (ActiveObjectConditionList.Count == 0)
            return;

        foreach(GimmickComponentBase condition in ActiveObjectConditionList)
        {
            if(condition.GimmickState == EGimmickObjectState.Complete)
            {
                ActiveObjectConditionList.Remove(condition);
                break;
            }
        }
    }

    private void CheckListOfReadyCondition()
    {
        if (GimmickReadyConditionList.Count == 0)
            return;

        foreach(GimmickComponentBase condition in GimmickReadyConditionList)
        {
            if(condition.GimmickState == EGimmickObjectState.Complete)
            {
                GimmickReadyConditionList.Remove(condition);
                break;
            }
        }
    }

#if UNITY_EDITOR

    protected virtual void Reset()
    {
        Editor_ResetComponentOperate();
    }

    public void Editor_SetGimmickComponentData(List<GimmickComponentBase> activeObjectConditionList, List<GimmickComponentBase> gimmickReadyConditionList)
    {
        this.ActiveObjectConditionList = activeObjectConditionList;
        this.GimmickReadyConditionList = gimmickReadyConditionList;
    }

    public virtual void Editor_ResetComponentOperate()
    {
        Editor_SetRigidbody();
    }

    public virtual void Editor_SetSpriteRenderer(Sprite sprite)
    {
        Sprite = Util.GetOrAddComponent<SpriteRenderer>(gameObject);
        Sprite.sprite = sprite;
    }

    protected virtual void Editor_SetRigidbody()
    {
        Rigidbody = Util.GetOrAddComponent<Rigidbody2D>(gameObject);

        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Rigidbody.freezeRotation = false;
    }

    public List<int> Editor_GetIntActiveObjectConditionList()
    {
        List<int> intActiveObjectConditionList = new();

        foreach (GimmickComponentBase gimmickComponentBase in ActiveObjectConditionList)
        {
            intActiveObjectConditionList.Add(gimmickComponentBase.gimmickObjectId);
        }

        return intActiveObjectConditionList;
    }

    public List<int> Editor_GetIntGimmickReadyConditionList()
    {
        List<int> intGimmickReadyConditionList = new();

        foreach (GimmickComponentBase gimmickComponentBase in GimmickReadyConditionList)
        {
            intGimmickReadyConditionList.Add(gimmickComponentBase.gimmickObjectId);
        }

        return intGimmickReadyConditionList;
    }

    public void Editor_AddActiveObjectCondition(GimmickComponentBase addTarget)
    {
        if (ActiveObjectConditionList.Contains(addTarget))
        {
            Debug.LogWarning($"이미 추가됨 : {addTarget.GimmickObjectId}번");
            return;
        }

        ActiveObjectConditionList.Add(addTarget);
    }

    public void Editor_RemoveActiveObjectCondition(GimmickComponentBase removeTarget)
    {
        if (!ActiveObjectConditionList.Contains(removeTarget))
        {
            Debug.LogWarning($"삭제 불가 (없음) : {removeTarget.GimmickObjectId}번");
            return;
        }

        ActiveObjectConditionList.Remove(removeTarget);
    }

    public void Editor_AddGimmickReadyConditionList(GimmickComponentBase addTarget)
    {
        if (GimmickReadyConditionList.Contains(addTarget))
        {
            Debug.LogWarning($"이미 추가됨 : {addTarget.GimmickObjectId}번");
            return;
        }

        GimmickReadyConditionList.Add(addTarget);
    }

    public void Editor_RemoveGimmickReadyConditionList(GimmickComponentBase removeTarget)
    {
        if (!GimmickReadyConditionList.Contains(removeTarget))
        {
            Debug.LogWarning($"삭제 불가 (없음) : {removeTarget.GimmickObjectId}번");
            return;
        }

        GimmickReadyConditionList.Remove(removeTarget);
    }
#endif
}
