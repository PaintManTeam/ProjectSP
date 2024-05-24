using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public interface IGimmick
{
    public EGimmickObjectState GimmickState { get; }
    public EGimmickType GimmickType { get; }
}

public class GimmickObject : BaseObject, IGimmick
{
    public EGimmickObjectState GimmickState { get; protected set; }
    public EGimmickType GimmickType { get; protected set; }

    public BoxCollider2D Collider { get; protected set; }
    
    private void Start()
    {
        SetInfo(0); // 임시
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Collider = GetComponent<BoxCollider2D>();

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        // GimmickState = EGimmickObjectState.StandBy;

        GimmickState = EGimmickObjectState.Ready; // 임시
        
    }

    public override Vector2 GetTopPosition()
    {
        return transform.position + Vector3.up * Collider.size.y / 2; // 콜라이더 세팅 시 세팅 해야 함
    }

    public override Vector2 GetBottomPosition()
    {
        return transform.position + Vector3.down * Collider.size.y / 2; // 콜라이더 세팅 시 세팅 해야 함
    }
}
