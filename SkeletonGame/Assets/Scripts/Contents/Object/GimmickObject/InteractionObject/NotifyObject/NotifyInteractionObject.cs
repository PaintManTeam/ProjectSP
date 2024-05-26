using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyInteractionObject : InitBase
{
    SpriteRenderer spriteRenderer;
    [SerializeField] MeshRenderer textMeshRenderer;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshRenderer = Util.FindChild(gameObject, "Text").GetComponent<MeshRenderer>();

        return true;
    }

    public void SetTarget(IInteraction target)
    {
        transform.position = target.WorldPosition;
        gameObject.SetActive(true);

        if (target is BaseObject baseObject)
        {
            int sortingOrder = baseObject.SpriteRender.sortingOrder + 1;

            textMeshRenderer.sortingOrder = sortingOrder;
            spriteRenderer.sortingOrder = sortingOrder;
        }
    }
}
