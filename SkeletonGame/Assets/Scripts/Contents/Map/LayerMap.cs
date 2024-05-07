using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LayerMap : InitBase
{
    [SerializeField] bool isFrontLayer = false;
    [SerializeField] int orderInLayer = 0;

    SpriteRenderer spriteRenderer;

    Transform _target;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = orderInLayer;

        float posZ = (isFrontLayer) ? -5 : 5;
        transform.position = new Vector3(this.transform.position.x, this.transform.position.y, posZ);

        return true;
    }

    // 비율에 따라 레이어 맵도 이동?
}
