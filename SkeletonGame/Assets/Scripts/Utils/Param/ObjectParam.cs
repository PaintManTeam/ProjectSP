using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectParam { }

public class InteractionParam : ObjectParam { }

public class PortalParam : InteractionParam
{
    public Action<BaseObject> onTeleportTarget;

    public PortalParam(Action<BaseObject> onTeleportTarget)
    {
        this.onTeleportTarget = onTeleportTarget;
    }
}
