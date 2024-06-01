using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public abstract class GimmickComponentBase : InitBase
{
    public EGimmickType GimmickType { get; protected set; }

    protected virtual void Reset()
    {
        // 얘를 상속받은 누군가를 떼고 붙일 때 뭔가 액션이 일어나게?

        // 1. 게임 오브젝트의 이름을 설정하여 이름 끝의 숫자로 해당 기믹 Id를 만듬?
    }

    public virtual void SetInfo(int templateId)
    {
        Debug.Log(templateId);
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        return true;
    }

    
}
