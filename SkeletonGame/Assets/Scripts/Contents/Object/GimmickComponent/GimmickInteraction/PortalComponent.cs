using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalComponent : GimmickComponentBase
{
    // 포탈 컴포넌트는 두 개의 포탈 오브젝트를 관리하므로, 상호작용 컴포넌트가 아닌 베이스를 상속받음

    [SerializeField, ReadOnly] PortalObject portalObject1;
    [SerializeField, ReadOnly] PortalObject portalObject2;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        if(portalObject1 == null || portalObject2 == null)
        {
            Debug.LogError("연결된 두 포탈 오브젝트가 없습니다.");
        }

        return true;
    }

    // 상호작용 가능 여부를 판단해서 포탈오브젝트를 제한해야 함

#if UNITY_EDITOR

    string portalObjectName = "PortalObject";
    public override void ResetComponentOperate()
    {
        base.ResetComponentOperate();

        List<PortalObject> portalObjectList = new List<PortalObject>();
        Transform[] myChildren = this.GetComponentsInChildren<Transform>();

        foreach(Transform child in myChildren)
        {
            PortalObject portalObject = child.gameObject.GetComponent<PortalObject>();
            if(portalObject != null)
                portalObjectList.Add(portalObject);
        }

        if (portalObjectList.Count > 2)
        {
            Debug.LogWarning($"{gameObject.name} : 포탈 오브젝트가 3개 이상 존재합니다.");
            return;
        }

        while (portalObjectList.Count != 2)
        {
            portalObjectList.Add(GeneratePortalObject(portalObjectName));
        }

        portalObject1 = portalObjectList[0];
        portalObject2 = portalObjectList[1];

        portalObject1.SetLinkedPortalObject(portalObject2);
        portalObject2.SetLinkedPortalObject(portalObject1);
    }

    public override void SetSpriteRenderer(Sprite sprite)
    {
        portalObject1?.SetSpriteRenderer(sprite);
        portalObject2?.SetSpriteRenderer(sprite);
    }

    private PortalObject GeneratePortalObject(string objectName)
    {
        GameObject go = Util.InstantiateObject(transform);
        go.AddComponent<PortalObject>().name = objectName;
        return go.GetComponentInParent<PortalObject>();
    }

#endif
}
