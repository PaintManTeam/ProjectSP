using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static Define;

public class PortalObject : BaseObject, IInteraction
{
    [SerializeField, ReadOnly] PortalObject linkedPortalObject;
    [SerializeField, ReadOnly] int portalId = 0;

    public BoxCollider2D Collider { get; protected set; }
    public EInteractionType InteractionType { get; protected set; }
    public Vector3 WorldPosition { get { return this.gameObject.transform.position; } }
    public SpriteRenderer Sprite { get; protected set; }

    public override Vector2 GetTopPosition()
    {
        return transform.position + Vector3.up * Collider.size.y / 2;
    }

    public override Vector2 GetBottomPosition()
    {
        return transform.position + Vector3.down * Collider.size.y / 2;
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        this.tag = ETag.Interaction.ToString();
        InteractionType = EInteractionType.Portal;
        Collider = GetComponent<BoxCollider2D>();
        Sprite = GetComponent<SpriteRenderer>();

        return true;
    }

    public override void SetInfo(int portalId)
    {
        this.portalId = portalId;


    }

    public bool IsInteractable()
    {
        if (this.gameObject.activeSelf == false)
            return false;

        return true;
    }

    public bool Interact(InteractionParam param = null)
    {
        if (IsInteractable() == false)
            return false;

        if (param is InteractionPortalParam portalParam)
        {
            portalParam.onTeleportTarget?.Invoke(linkedPortalObject);
        }

        return true;
    }


#if UNITY_EDITOR
    private void Reset()
    {
        Editor_SetComponents();
    }

    private void Editor_SetComponents()
    {
        //Collider
        Collider = Util.GetOrAddComponent<BoxCollider2D>(gameObject);
        Collider.isTrigger = true;
    }

    public void Editor_SetSpriteRenderer(Sprite sprite)
    {
        Sprite = Util.GetOrAddComponent<SpriteRenderer>(gameObject);
        Sprite.sprite = sprite;
    }

    public void Editor_SetLinkedPortalObject(PortalObject linkedPortalObject)
    {
        this.linkedPortalObject = linkedPortalObject;
    }
#endif
}
