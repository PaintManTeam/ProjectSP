using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParam { }

public class InteractionParam : ObjectParam { }

public class PortalParam : InteractionParam
{
    public Action<Transform> onTeleportTarget;

    public PortalParam(Action<Transform> onTeleportTarget)
    {
        this.onTeleportTarget = onTeleportTarget;
    }
}
