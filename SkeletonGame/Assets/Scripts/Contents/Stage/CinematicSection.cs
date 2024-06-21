using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicSection : StageSectionBase
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;



        return true;
    }

    protected override void Reset()
    {
        base.Reset();
    }

#if UNITY_EDITOR

    public override void SaveSectionData()
    {

    }

    public override void LoadSectionData()
    {

    }

#endif
}
