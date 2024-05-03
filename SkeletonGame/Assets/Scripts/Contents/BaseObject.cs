using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class BaseObject : InitBase
{
    public EObjectType ObjectType { get; protected set; } = EObjectType.None;
    public Collider2D Collider { get; private set; }
    public Rigidbody2D RigidBody { get; private set; }
    public SpriteRenderer SpriteRender { get; protected set; }

    bool lookLeft = true;
    public bool LookLeft
    {
        get { return lookLeft; }
        set
        {
            lookLeft = value;
            Flip(!value);
        }
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Collider = gameObject.GetComponent<Collider2D>();
        SpriteRender = GetComponent<SpriteRenderer>();
        RigidBody = GetComponent<Rigidbody2D>();

        return true;
    }

    public void Flip(bool flag)
    {
        if (SpriteRender == null)
            return;

        SpriteRender.flipX = flag;
    }
}
