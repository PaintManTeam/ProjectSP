using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerInteractionRange : InitBase
{
    CircleCollider2D circleCollider;
    List<IInteraction> interactionRangeList;
    Action<IInteraction> OnDetectTargetChanged;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        circleCollider = GetComponent<CircleCollider2D>();
        interactionRangeList = new List<IInteraction>();

        return true;
    }

    public void SetInfo(Action<IInteraction> onDetectionTargetChanged, float colliderCenter, float radius)
    {
        this.OnDetectTargetChanged = onDetectionTargetChanged;
        transform.localPosition = new Vector2(0, colliderCenter);
        circleCollider.radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ETag.Interaction.ToString())
        {
            IInteraction interactionTarget = collision.GetComponent<InteractionObject>();
            
            if(interactionTarget != null && !interactionRangeList.Contains(interactionTarget))
            {
                interactionRangeList.Add(interactionTarget);
                OnDetectTargetChanged(FindClosestInRange());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == ETag.Interaction.ToString())
        {
            IInteraction interactionTarget = collision.GetComponent<IInteraction>();

            if (interactionTarget != null && interactionRangeList.Contains(interactionTarget))
            {
                interactionRangeList.Remove(interactionTarget);
                OnDetectTargetChanged(FindClosestInRange());
            }
        }
    }

    private IInteraction FindClosestInRange()
    {
        if (interactionRangeList.Count == 0)
            return null;

        if (interactionRangeList.Count == 1)
            return interactionRangeList[0];

        InteractionObject target = null;

        float bestDistanceSqr = float.MaxValue;

        foreach(InteractionObject interactionObject in interactionRangeList)
        {
            // 상호작용 가능 상태인지 확인
            if (interactionObject.GimmickState != EGimmickObjectState.Ready)
                continue;

            Vector3 dir = interactionRangeList[0].WorldPosition - transform.position;
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
