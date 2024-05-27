using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectParam { }

public class InteractionParam : ObjectParam { }

public class InteractionPortalParam : InteractionParam
{
    public Action<BaseObject> onTeleportTarget;

    public InteractionPortalParam(Action<BaseObject> onTeleportTarget)
    {
        this.onTeleportTarget = onTeleportTarget;
    }
}

public class InteractionDialogueParam : InteractionParam
{
    public Action onEndDialogue;

    public InteractionDialogueParam(Action onEndDialogue)
    {
        this.onEndDialogue = onEndDialogue;
    }
}