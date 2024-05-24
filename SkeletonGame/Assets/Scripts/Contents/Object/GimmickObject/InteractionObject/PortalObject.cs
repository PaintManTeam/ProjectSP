using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalObject : InteractionObject
{
    [SerializeField] BaseObject teleportTarget;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        InteractionType = Define.EInteractionType.Portal;

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);
        

    }

    public override bool Interact(InteractionParam param = null)
    {
        if (base.Interact(param) == false)
            return false;

        if(param is PortalParam portalParam)
        {

        }
        
        return true;
    }
}
