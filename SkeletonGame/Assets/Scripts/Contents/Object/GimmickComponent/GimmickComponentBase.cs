using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public interface IGimmickComponent
{
    public EGimmickObjectState GimmickState { get; }
    public EGimmickType GimmickType { get; }
}

public abstract class GimmickComponentBase : InitBase, IGimmickComponent
{
    public EGimmickObjectState GimmickState { get; protected set; }
    public EGimmickType GimmickType { get; protected set; }

    public SpriteRenderer Sprite { get; protected set; }
    public Rigidbody2D Rigidbody { get; protected set; }

    private void Start()
    {
        SetInfo(0); // 임시
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Rigidbody = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();

        return true;
    }

    public virtual void SetInfo(int templateId)
    {
        GimmickState = EGimmickObjectState.Ready; // 임시
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

    public void SetSpriteRenderer(Sprite sprite)
    {
        Sprite = Util.GetOrAddComponent<SpriteRenderer>(gameObject);

        Sprite.sprite = sprite;
    }

    protected void SetRigidbody()
    {
        Rigidbody = Util.GetOrAddComponent<Rigidbody2D>(gameObject);

        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Rigidbody.freezeRotation = false;
    }
#endif
}
