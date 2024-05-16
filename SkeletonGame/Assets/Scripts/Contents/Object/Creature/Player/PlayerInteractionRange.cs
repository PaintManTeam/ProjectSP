using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerInteractionRange : InitBase
{
    CircleCollider2D circleCollider;

    List<InteractionObject> interactionObjectList = new List<InteractionObject>();
    Action<InteractionObject> onDetectObjectChanged;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        circleCollider = GetComponent<CircleCollider2D>();

        return true;
    }

    public void SetInfo(Action<InteractionObject> onDetectionObjectChanged, float colliderCenter, float radius)
    {
        this.onDetectObjectChanged = onDetectionObjectChanged;
        transform.localPosition = new Vector2(0, colliderCenter);
        circleCollider.radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ETag.Interaction.ToString())
        {
            InteractionObject interactionObject = collision.GetComponent<InteractionObject>();
            
            if(interactionObject != null && !interactionObjectList.Contains(interactionObject))
            {
                interactionObjectList.Add(interactionObject);
                onDetectObjectChanged(FindClosestInRange());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == ETag.Interaction.ToString())
        {
            InteractionObject interactionObject = collision.GetComponent<InteractionObject>();

            if (interactionObject != null && interactionObjectList.Contains(interactionObject))
            {
                interactionObjectList.Remove(interactionObject);
                onDetectObjectChanged(FindClosestInRange());
            }
        }
    }

    private InteractionObject FindClosestInRange()
    {
        if (interactionObjectList.Count == 0)
            return null;

        InteractionObject target = null;

        float bestDistanceSqr = float.MaxValue;

        foreach(InteractionObject interactionObject in interactionObjectList)
        {
            // 상호작용 가능 상태인지 확인
            if (interactionObject.GimmickState != EGimmickObjectState.Ready)
                continue;

            Vector3 dir = interactionObjectList[0].transform.position - transform.position;
            float distToTargetSqr = dir.sqrMagnitude;

            // 이미 더 좋은 후보를 찾았으면 스킵.
            if (distToTargetSqr > bestDistanceSqr)
                continue;

            target = interactionObject;
            bestDistanceSqr = distToTargetSqr;
        }

        return target;
    }
}
