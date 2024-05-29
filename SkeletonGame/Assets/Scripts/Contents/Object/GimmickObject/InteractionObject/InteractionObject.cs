using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public interface IInteraction
{
    public EInteractionType InteractionType { get; }
    public Vector3 WorldPosition { get; }
    public bool Interact(InteractionParam param = null);
}

public class InteractionObject : GimmickObject, IInteraction
{
    // 임시 테스트
    [SerializeField] InteractionComponentBase[] testArray;

    public EInteractionType InteractionType { get; protected set; }
    public Vector3 WorldPosition { get { return this.transform.position; } }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        InteractionType = EInteractionType.EndMotion;

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        GimmickType = EGimmickType.Interaction;
    }

    public virtual bool Interact(InteractionParam param = null)
    {
        if (GimmickState != EGimmickObjectState.Ready)
            return false;

        

        return true;
    }
}
