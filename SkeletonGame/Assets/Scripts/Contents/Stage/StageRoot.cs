using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRoot : InitBase
{
    [SerializeField] 
    List<StageComponentBase> StageSectionList;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        

        return true;
    }



}
