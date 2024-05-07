using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Creature : BaseObject
{
    public ECreatureState CreatureState { get; protected set; }
    public ECreatureType CreatureType { get; protected set; }

    protected Rigidbody2D Rigid { get; private set; }
    protected Animator animator;

    bool lookLeft = true;
    public bool LookLeft
    {
        get { return lookLeft; }
        set
        {
            lookLeft = value;
            Flip(value);
        }
    }

    private void Start()
    {
        SetInfo(0); // 임시 (오브젝트 매니저에서 스폰 시 수행) 
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        return true;
    }

    public virtual void SetInfo(int templateID)
    {
        CreatureState = ECreatureState.Idle;
    }

    #region Rigid
    protected void SetRigidVelocityX(float x)
    {
        Rigid.velocity = new Vector2(x, Rigid.velocity.y);
    }

    protected void SetRigidVelocityY(float y)
    {
        Rigid.velocity = new Vector2(Rigid.velocity.x, y);
    }

    protected void SetRigidVelocityZero()
    {
        Rigid.velocity = Vector3.zero;
    }
    #endregion

    #region Animation
    protected void PlayAnimation(ECreatureState state)
    {
        if (animator == null)
            return;

        animator.Play(state.ToString());
    }

    public bool IsState(ECreatureState state)
    {
        if (animator == null)
            return false;

        return IsState(animator.GetCurrentAnimatorStateInfo(0), state);
    }

    public bool IsState(AnimatorStateInfo stateInfo, ECreatureState state)
    {
        return stateInfo.IsName(state.ToString());
    }

    public bool IsEndState(AnimatorStateInfo stateInfo)
    {
        return stateInfo.normalizedTime >= 1.0f;
    }
    #endregion
}
