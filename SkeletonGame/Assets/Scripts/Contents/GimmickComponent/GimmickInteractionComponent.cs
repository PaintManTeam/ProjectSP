using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public interface IInteraction
{
    public EInteractionType InteractionType { get; }
    public Vector3 WorldPosition { get; }
    public bool IsInteractable();
    public bool Interact(InteractionParam param = null);
}

public class GimmickInteractionComponent : GimmickComponentBase, IInteraction
{
    public BoxCollider2D Collider { get; protected set; }
    public EInteractionType InteractionType { get; protected set; }
    public Vector3 WorldPosition { get { return this.gameObject.transform.position; } }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Collider = GetComponent<BoxCollider2D>();

        return true;
    }

    public bool IsInteractable()
    {
        if (GimmickState != EGimmickObjectState.Ready)
            return false;

        return true;
    }

    public virtual bool Interact(InteractionParam param = null)
    {
        return IsInteractable();
    }

#if UNITY_EDITOR
    public override void ResetComponentOperate()
    {
        base.ResetComponentOperate();

        this.tag = ETag.Interaction.ToString();
        SetCollider();
    }

    private void SetCollider()
    {
        Collider = Util.GetOrAddComponent<BoxCollider2D>(gameObject);
        Collider.isTrigger = true;
    }
#endif
}