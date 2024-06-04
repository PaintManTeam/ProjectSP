using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public abstract class GimmickComponentBase : InitBase
{
    public EGimmickType GimmickType { get; protected set; }

    public Rigidbody2D Rigidbody { get; protected set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public virtual void SetInfo(int templateId)
    {
        Debug.Log(templateId);
    }

#if UNITY_EDITOR
    protected virtual void Reset()
    {
        AddComponentOperate();
    }

    public virtual void AddComponentOperate()
    {

    }

    public virtual void RemoveComponentOperate()
    {
        DestroyImmediate(this);
    }

    protected void SetRigidbody()
    {
        Rigidbody = Util.GetOrAddComponent<Rigidbody2D>(gameObject);

        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Rigidbody.freezeRotation = false;
    }
#endif
}
