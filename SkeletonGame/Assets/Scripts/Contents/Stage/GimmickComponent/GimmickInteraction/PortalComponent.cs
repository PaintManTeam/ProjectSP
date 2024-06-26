using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 서로 연결된 상호작용 컴포넌트를 상속받은 PortalObject를 관리
public class PortalComponent : GimmickComponentBase
{
    [Header ("포탈 컴포넌트 정보")]
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
    public override void Editor_ResetComponentOperate()
    {
        base.Editor_ResetComponentOperate();

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
            portalObjectList.Add(Editor_GeneratePortalObject(portalObjectName));
        }

        portalObject1 = portalObjectList[0];
        portalObject2 = portalObjectList[1];

        portalObject1.Editor_SetLinkedPortalObject(portalObject2);
        portalObject2.Editor_SetLinkedPortalObject(portalObject1);
    }

    public override void Editor_SetSpriteRenderer(Sprite sprite)
    {
        portalObject1?.Editor_SetSpriteRenderer(sprite);
        portalObject2?.Editor_SetSpriteRenderer(sprite);
    }

    private PortalObject Editor_GeneratePortalObject(string objectName)
    {
        GameObject go = Util.Editor_InstantiateObject(transform);
        go.AddComponent<PortalObject>().name = objectName;
        return go.GetComponentInParent<PortalObject>();
    }

#endif
}
