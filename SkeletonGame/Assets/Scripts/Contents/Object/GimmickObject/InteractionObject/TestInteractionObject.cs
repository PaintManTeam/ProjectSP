using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractionObject : InteractionObject
{
    public override bool Interact(InteractionParam param)
    {
        if (base.Interact(param) == false)
            return false;

        Managers.Resource.Destroy(gameObject);

        return true;
    }
}
