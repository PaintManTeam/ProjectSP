using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class GimmickComponentBase : InitBase
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        return true;
    }


}
