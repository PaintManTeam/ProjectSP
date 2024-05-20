using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public interface IInteraction
{
    public EInteractionType InteractionType { get; }
    public Vector3 WorldPosition { get; }

    public bool Interact();
}

public class InteractionObject : GimmickObject, IInteraction
{
    public EInteractionType InteractionType { get; protected set; }
    public Vector3 WorldPosition { get { return this.transform.position; } }

    protected NotifyInteractionObject notifyObject = null;
    SpriteRenderer spriteRenderer;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        spriteRenderer = GetComponent<SpriteRenderer>();

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        GimmickType = EGimmickType.Interaction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GimmickState != EGimmickObjectState.Ready)
            return;

        PlayerInteractionRange playerInteractionRange = collision.GetComponent<PlayerInteractionRange>();
        
        if (playerInteractionRange == null)
            return;

        if(notifyObject == null)
        {
            notifyObject = Managers.Resource.Instantiate<NotifyInteractionObject>($"{PrefabPath.OBJECT_PATH}").GetComponent<NotifyInteractionObject>();
            notifyObject.transform.position = this.transform.position;
            notifyObject.SetInfo(spriteRenderer.sortingOrder + 1); // 임시
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInteractionRange playerInteractionRange = collision.GetComponent<PlayerInteractionRange>();

        if (notifyObject != null && playerInteractionRange != null)
            Managers.Resource.Destroy(notifyObject.gameObject);
    }

    public virtual bool Interact()
    {
        if (GimmickState != EGimmickObjectState.Ready)
            return false;

        if (notifyObject != null)
            Managers.Resource.Destroy(notifyObject.gameObject);

        return true;
    }
}
