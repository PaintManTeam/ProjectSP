using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class StageComponentBase : InitBase
{
    public EStageSectionType SectionType {  get; protected set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public virtual void SetInfo(int templateID)
    {

    }

    
}
