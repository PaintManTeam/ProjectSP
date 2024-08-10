using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStageCorner : InitBase
{
    [SerializeField, ReadOnly] BaseMap map;
    [SerializeField, ReadOnly] Transform startPoint;
    
    public int CornerId { get; protected set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Reset();

        return true;
    }

    public virtual void SetInfo(int cornerId)
    {
        CornerId = cornerId;

    }

    private void Reset()
    {
        map = Util.FindChild(this.gameObject, "Map").GetComponent<BaseMap>();

        if (map == null)
            Debug.LogWarning($"{this.gameObject.name} 에서 Map을 찾지 못했습니다.");
    }

    public void StartCorner()
    {

    }
}