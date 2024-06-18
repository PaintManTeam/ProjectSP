using System;
using System.Collections;
using System.Collections.Generic;
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
                GimmickObjectId = int.Parse(strs[strs.Length - 1]);
            }

            return gimmickObjectId;
        }
        private set { gimmickObjectId = value; }
    }

    [Header("오브젝트 활성화 조건")]
    [SerializeField, ReadOnly] List<GimmickComponentBase> activeObjectConditionList;

    [Header("기믹 준비 조건")]
    [SerializeField, ReadOnly] List<GimmickComponentBase> gimmickReadyConditionList;

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

        UpdateGimmickState();
        
        return true;
    } 

    private void UpdateGimmickState()
    {
        // 오브젝트 활성화
        this.gameObject.SetActive(activeObjectConditionList.Count == 0);

        // 오브젝트 레디 조건
        GimmickState = (gimmickReadyConditionList.Count == 0) ?
            EGimmickObjectState.Ready : EGimmickObjectState.StandBy;
    }

    public void CheckListOfConditionToRemove()
    {
        if (GimmickState != EGimmickObjectState.StandBy)
            return;

        CheckListOfActiveCondition();
        CheckListOfReadyCondition();

        UpdateGimmickState();
    }

    private void CheckListOfActiveCondition()
    {
        if (activeObjectConditionList.Count == 0)
            return;

        foreach(GimmickComponentBase condition in activeObjectConditionList)
        {
            if(condition.GimmickState == EGimmickObjectState.Complete)
            {
                activeObjectConditionList.Remove(condition);
                break;
            }
        }
    }

    private void CheckListOfReadyCondition()
    {
        if (gimmickReadyConditionList.Count == 0)
            return;

        foreach(GimmickComponentBase condition in gimmickReadyConditionList)
        {
            if(condition.GimmickState == EGimmickObjectState.Complete)
            {
                gimmickReadyConditionList.Remove(condition);
                break;
            }
        }
    }

#if UNITY_EDITOR
    
    protected virtual void Reset()
    {
        ResetComponentOperate();
    }

    public virtual void ResetComponentOperate()
    {
        SetRigidbody();

        if(activeObjectConditionList.Count == 0)
            activeObjectConditionList = new();

        if(gimmickReadyConditionList.Count == 0)
            gimmickReadyConditionList = new();
    }

    public virtual void SetSpriteRenderer(Sprite sprite)
    {
        Sprite = Util.GetOrAddComponent<SpriteRenderer>(gameObject);
        Sprite.sprite = sprite;
    }

    protected virtual void SetRigidbody()
    {
        Rigidbody = Util.GetOrAddComponent<Rigidbody2D>(gameObject);

        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Rigidbody.freezeRotation = false;
    }

    public void AddActiveObjectCondition(GimmickComponentBase addTarget)
    {
        if(activeObjectConditionList.Contains(addTarget))
        {
            Debug.LogWarning($"이미 추가됨 : {addTarget.GimmickObjectId}번");
            return;
        }

        activeObjectConditionList.Add(addTarget);
    }

    public void RemoveActiveObjectCondition(GimmickComponentBase removeTarget)
    {
        if (!activeObjectConditionList.Contains(removeTarget))
        {
            Debug.LogWarning($"삭제 불가 (없음) : {removeTarget.GimmickObjectId}번");
            return;
        }

        activeObjectConditionList.Remove(removeTarget);
    }
    
    public void AddGimmickReadyConditionList(GimmickComponentBase addTarget)
    {
        if (gimmickReadyConditionList.Contains(addTarget))
        {
            Debug.LogWarning($"이미 추가됨 : {addTarget.GimmickObjectId}번");
            return;
        }

        activeObjectConditionList.Add(addTarget);
    }

    public void RemoveGimmickReadyConditionList(GimmickComponentBase removeTarget)
    {
        if (!gimmickReadyConditionList.Contains(removeTarget))
        {
            Debug.LogWarning($"삭제 불가 (없음) : {removeTarget.GimmickObjectId}번");
            return;
        }

        gimmickReadyConditionList.Remove(removeTarget);
    }
#endif
}
