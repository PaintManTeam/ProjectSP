using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicSection : StageSectionBase
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SectionType = Define.EStageSectionType.CinematicSection;

        return true;
    }

    public override void LoadSectionData()
    {

    }

#if UNITY_EDITOR

    protected override void Reset()
    {
        base.Reset();
    }

    public override void Editor_SaveSectionData()
    {

    }


#endif
}
