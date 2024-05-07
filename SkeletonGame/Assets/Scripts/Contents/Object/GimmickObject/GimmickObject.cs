using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GimmickObject : BaseObject
{
    public EGimmickObjectState GimmickState { get; protected set; }
    public EGimmickObjectType GimmickType { get; protected set; }

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
}
