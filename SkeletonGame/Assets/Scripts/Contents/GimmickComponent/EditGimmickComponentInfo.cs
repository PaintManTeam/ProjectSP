using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditGimmickComponentInfo : MonoBehaviour
{
#if UNITY_EDITOR

    // 첫번째 Id -> 두번째 Id 로 이벤트 발생
    Action<int, int> onAddActiveObjectCondition;
    Action<int, int> onRemoveActiveObjectCondition;
    Action<int, int> onAddGimmickReadyCondition;
    Action<int, int> onRemoveGimmickReadyCondition;

    public void SetInfo(Action<int, int> onAddActiveObjectCondition, Action<int, int> onRemoveActiveObjectCondition,
        Action<int, int> onAddGimmickReadyCondition, Action<int, int> onRemoveGimmickReadyCondition)
    {
        this.onAddActiveObjectCondition = onAddActiveObjectCondition;
        this.onRemoveActiveObjectCondition = onRemoveActiveObjectCondition;
        this.onAddGimmickReadyCondition = onAddGimmickReadyCondition;
        this.onRemoveGimmickReadyCondition = onRemoveGimmickReadyCondition;
    }

    public void AddActiveObjectCondition(int requestObjectId, int receiveObjectId)
    {
        if (onAddActiveObjectCondition != null)
            onAddActiveObjectCondition.Invoke(requestObjectId, receiveObjectId);
        else
            Debug.LogWarning("오브젝트가 존재하는 섹션을 갱신해주세요.");
    }

    public void RemoveActiveObjectCondition(int requestObjectId, int receiveObjectId)
    {
        if (onRemoveActiveObjectCondition != null)
            onRemoveActiveObjectCondition.Invoke(requestObjectId, receiveObjectId);
        else
            Debug.LogWarning("오브젝트가 존재하는 섹션을 갱신해주세요.");
    }

    public void AddGimmickReadyConditionList(int requestObjectId, int receiveObjectId)
    {
        if (onAddGimmickReadyCondition != null)
            onAddGimmickReadyCondition.Invoke(requestObjectId, receiveObjectId);
        else
            Debug.LogWarning("오브젝트가 존재하는 섹션을 갱신해주세요.");
    }

    public void RemoveGimmickReadyConditionList(int requestObjectId, int receiveObjectId)
    {
        if (onRemoveGimmickReadyCondition != null)
            onRemoveGimmickReadyCondition.Invoke(requestObjectId, receiveObjectId);
        else
            Debug.LogWarning("오브젝트가 존재하는 섹션을 갱신해주세요.");
    }

#endif
}
