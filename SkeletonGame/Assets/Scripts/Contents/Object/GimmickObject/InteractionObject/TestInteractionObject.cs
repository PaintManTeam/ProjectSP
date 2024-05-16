using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractionObject : InteractionObject
{
    public override bool Interact()
    {
        if (base.Interact() == false)
            return false;

        Managers.Resource.Destroy(gameObject);

        return true;
    }
}
