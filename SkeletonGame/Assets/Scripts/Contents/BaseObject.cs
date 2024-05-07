using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class BaseObject : InitBase
{
    public EObjectType ObjectType { get; protected set; } = EObjectType.None;
    protected Collider2D Collider { get; private set; }
    protected Rigidbody2D RigidBody { get; private set; }
    public SpriteRenderer SpriteRender { get; protected set; }

    bool lookLeft = true;
    public bool LookLeft
    {
        get { return lookLeft; }
        set
        {
            lookLeft = value;
            Flip(value);
        }
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Collider = gameObject.GetComponent<Collider2D>();
        RigidBody = GetComponent<Rigidbody2D>();
        SpriteRender = GetComponent<SpriteRenderer>();

        return true;
    }

    protected virtual void Flip(bool flag)
    {
        if (SpriteRender == null)
            return;

        SpriteRender.flipX = flag;
    }

    protected void SetRigidVelocityX(float x)
    {
        RigidBody.velocity = new Vector2(x, RigidBody.velocity.y);
    }

    protected void SetRigidVelocityY(float y)
    {
        RigidBody.velocity = new Vector2(RigidBody.velocity.x, y);
    }

    protected void SetRigidVelocityZero()
    {
        RigidBody.velocity = Vector3.zero;
    }
}
