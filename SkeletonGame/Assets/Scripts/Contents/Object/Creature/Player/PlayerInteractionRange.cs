using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerInteractionRange : InitBase
{
    CircleCollider2D circleCollider;
    List<IInteraction> interactionRangeList;
    Action<IInteraction> onDetectTargetChanged;

    private IInteraction _interactionTarget = null;
    public IInteraction InteractionTarget
    {
        get { return _interactionTarget; }
        private set
        {
            if(value == null)
            {
                _interactionTarget = null;
                notifyObject.gameObject.SetActive(false);
                return;
            }

            if (ReferenceEquals(_interactionTarget, value))
                return;
            
            _interactionTarget = value;
            onDetectTargetChanged?.Invoke(_interactionTarget);

            notifyObject?.SetTarget(_interactionTarget);
        }
    }

    [SerializeField] private NotifyInteractionObject notifyObject = null;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        circleCollider = GetComponent<CircleCollider2D>();
        interactionRangeList = new List<IInteraction>();

        notifyObject = Managers.Resource.Instantiate<NotifyInteractionObject>($"{PrefabPath.OBJECT_PATH}").GetComponent<NotifyInteractionObject>();
        notifyObject.gameObject.SetActive(false);

        return true;
    }

    public void SetInfo(Action<IInteraction> onDetectionTargetChanged, float colliderCenter, float radius)
    {
        this.onDetectTargetChanged = onDetectionTargetChanged;
        transform.localPosition = new Vector2(0, colliderCenter);
        circleCollider.radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ETag.Interaction.ToString())
        {
            IInteraction interactionTarget = collision.GetComponent<IInteraction>();
            
            if(interactionTarget != null && !interactionRangeList.Contains(interactionTarget))
            {
                interactionRangeList.Add(interactionTarget);
                ChangeTargetCheck();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == ETag.Interaction.ToString())
        {
            IInteraction interactionTarget = collision.GetComponent<IInteraction>();

            if (interactionRangeList.Contains(interactionTarget))
            {
                interactionRangeList.Remove(interactionTarget);
                ChangeTargetCheck();
            }
        }
    }
    
    public void Interactioncomplete(IInteraction interaction)
    {
        if (interactionRangeList.Contains(interaction))
            interactionRangeList.Remove(interaction);
    }

    private void ChangeTargetCheck()
    {
        coChangeTargetCheck ??= StartCoroutine(CoChangeTargetCheck());
    }

    Coroutine coChangeTargetCheck = null;
    private IEnumerator CoChangeTargetCheck()
    {
        if (interactionRangeList.Count == 0)
            InteractionTarget = null;
        else if (interactionRangeList.Count == 1)
            InteractionTarget = interactionRangeList[0];

        while (interactionRangeList.Count > 1)
        {
            InteractionTarget = FindClosestInRange();
            yield return new WaitForSeconds(0.1f);
        }

        coChangeTargetCheck = null;
    }

    private IInteraction FindClosestInRange()
    {
        IInteraction target = null;

        float bestDistanceSqr = float.MaxValue;

        foreach(IInteraction interactionTarget in interactionRangeList)
        {
            // 상호작용 가능 상태인지 확인
            if (interactionTarget.IsInteractable() == false)
                continue;

            Vector3 dir = interactionTarget.WorldPosition - transform.position;
            float distToTargetSqr = dir.sqrMagnitude;

            // 이미 더 좋은 후보를 찾았으면 스킵.
            if (distToTargetSqr > bestDistanceSqr)
                continue;

            target = interactionTarget;
            bestDistanceSqr = distToTargetSqr;
        }

        return target;
    }


}
