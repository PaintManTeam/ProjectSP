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

public class InteractionComponent : GimmickComponentBase, IInteraction
{
    public EInteractionType InteractionType { get; protected set; }
    public Vector3 WorldPosition { get { return this.gameObject.transform.position; } }

    public bool Interact(InteractionParam param = null)
    {


        return true;
    }
}