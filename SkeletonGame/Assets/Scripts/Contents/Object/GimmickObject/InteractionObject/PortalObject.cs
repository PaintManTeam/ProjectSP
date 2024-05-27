using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PortalObject : InteractionObject
{
    // 임시 ( 나중엔, 배치된 프리팹 그룹 내에 설정된 고유 ID 값을 가질 예정 )
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

        if(param is InteractionPortalParam portalParam)
        {
            portalParam.onTeleportTarget?.Invoke(teleportTarget);
        }
        
        return true;
    }
}
