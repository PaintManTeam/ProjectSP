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
        textMeshRenderer.sortingOrder = spriteRenderer.sortingOrder;

        return true;
    }

    public void SetInfo(int sortingOrder)
    {
        textMeshRenderer.sortingOrder = sortingOrder;
        spriteRenderer.sortingOrder = sortingOrder;
    }
}
