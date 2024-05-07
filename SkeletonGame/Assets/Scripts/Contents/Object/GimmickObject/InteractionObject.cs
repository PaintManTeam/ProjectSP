using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class InteractionObject : GimmickObject
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        GimmickType = EGimmickObjectType.Interaction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GimmickState != EGimmickObjectState.Ready)
            return;
        
        // 근처에 상호작용 가능 UI 띄우기
    }

    public void Interact()
    {
        if (GimmickState != EGimmickObjectState.Ready)
            return;

    }
}
