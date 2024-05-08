using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GimmickObject : BaseObject
{
    public EGimmickObjectState GimmickState { get; protected set; }
    public EGimmickObjectType GimmickType { get; protected set; }

    // 아마 무조건 박스 콜라이더가 되지 않을까?
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public virtual void SetInfo(int templateID)
    {
        GimmickState = EGimmickObjectState.StandBy;

        
    }

    public override Vector2 GetTopPosition()
    {
        return transform.position; // 콜라이더 세팅 시 세팅 해야 함
    }
}
