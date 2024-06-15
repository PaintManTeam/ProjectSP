using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickCollisionComponent : GimmickComponentBase
{
    public BoxCollider2D Collider { get; protected set; }

    protected override void Reset()
    {
        base.Reset();



        Collider = Util.GetOrAddComponent<BoxCollider2D>(gameObject);
    }
}
