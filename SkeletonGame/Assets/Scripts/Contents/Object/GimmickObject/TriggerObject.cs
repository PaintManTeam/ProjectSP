using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class TriggerObject : GimmickObject
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


    }
}
