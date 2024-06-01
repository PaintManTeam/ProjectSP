using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickPuzzleComponent : StageComponentBase
{
    [SerializeField]
    List<GimmickComponentBase> gimmickComponentList;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SectionType = Define.EStageSectionType.GimmickPuzzle;

        

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);


    }

    
}
