using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInteractionComponent : GimmickInteractionComponent
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public override bool Interact(InteractionParam param = null)
    {
        if(base.Interact(param) == false)
            return false;

        Managers.Resource.Destroy(gameObject);

        return true;
    }
}
