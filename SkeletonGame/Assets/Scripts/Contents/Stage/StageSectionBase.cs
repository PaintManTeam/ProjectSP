using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageSectionBase : InitBase
{
    public Transform PlayerStartPoint { get; protected set; }

    protected virtual void Reset()
    {
        PlayerStartPoint = gameObject.transform.Find("PlayerStartPoint");
        
        if(PlayerStartPoint == null)
        {
            GameObject go = Util.InstantiateObject(transform);
            go.name = "PlayerStartPoint";
            PlayerStartPoint = go.transform;
        }
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;



        return true;
    }


}
