using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class BaseObject : InitBase
{
    public EObjectType ObjectType { get; protected set; } = EObjectType.None;
    public SpriteRenderer SpriteRender { get; protected set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SpriteRender = GetComponent<SpriteRenderer>();

        return true;
    }

    protected virtual void Flip(bool flag)
    {
        if (SpriteRender == null)
            return;

        SpriteRender.flipX = flag;
    }

    public virtual Vector2 GetCenterPosition()
    {
        return transform.position;
    }
}
