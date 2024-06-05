using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public interface IInteraction
{
    public EInteractionType InteractionType { get; }
    public Vector3 WorldPosition { get; }
    public bool Interact(InteractionParam param = null);
}

public class GimmickInteractionComponent : GimmickComponentBase, IInteraction
{
    public BoxCollider2D Collider { get; protected set; }

    public EInteractionType InteractionType { get; protected set; }
    public Vector3 WorldPosition { get { return this.gameObject.transform.position; } }

    

    public bool Interact(InteractionParam param = null)
    {


        return true;
    }

#if UNITY_EDITOR
    public override void AddComponentOperate()
    {
        base.AddComponentOperate();

        SetCollider();
        SetRigidbody();
    }

    public override void RemoveComponentOperate()
    {
        DestroyImmediate(Rigidbody);
        DestroyImmediate(Collider);

        base.RemoveComponentOperate();
    }

    private void SetCollider()
    {
        Collider = Util.GetOrAddComponent<BoxCollider2D>(gameObject);

        Collider.isTrigger = true;
    }
#endif
}