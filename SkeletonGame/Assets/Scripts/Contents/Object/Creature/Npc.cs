using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Creature
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;


        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        CreatureType = Define.ECreatureType.Npc;
    }
}
