using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static Define;

public class PortalObject : InitBase, IInteraction
{
    [SerializeField, ReadOnly] PortalObject linkedPortalObject;
    
    public BoxCollider2D Collider { get; protected set; }
    public EInteractionType InteractionType { get; protected set; }
    public Vector3 WorldPosition { get { return this.gameObject.transform.position; } }
    public SpriteRenderer Sprite { get; protected set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public bool Interact(InteractionParam param = null)
    {
        // 포탈 컴포넌트에게 위탁하여 처리?

        return true;
    }

#if UNITY_EDITOR
    private void Reset()
    {
        SetComponents();
    }

    private void SetComponents()
    {
        //Collider
        Collider = Util.GetOrAddComponent<BoxCollider2D>(gameObject);
        Collider.isTrigger = true;
    }

    public void SetSpriteRenderer(Sprite sprite)
    {
        Sprite = Util.GetOrAddComponent<SpriteRenderer>(gameObject);
        Sprite.sprite = sprite;
    }

    public void SetLinkedPortalObject(PortalObject linkedPortalObject)
    {
        this.linkedPortalObject = linkedPortalObject;
    }
#endif
}
