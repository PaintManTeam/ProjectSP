using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using static Define;

public class Character : BaseObject
{
    public ECharacterState CharacterState { get; protected set; }

    private Animator animator;

    // 임시
    protected float Speed = 5.0f;
    protected float JumpPower = 10.0f;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        animator = GetComponent<Animator>();

        return true;
    }

    public virtual void SetInfo()
    {

        CharacterState = ECharacterState.Idle;

    }

    #region Animation
    protected void PlayAnimation(ECharacterState state)
    {
        if (animator == null)
            return;

        animator.Play(state.ToString());
    }

    public bool IsState(ECharacterState state)
    {
        if (animator == null)
            return false;

        return IsState(animator.GetCurrentAnimatorStateInfo(0), state);
    }

    public bool IsState(AnimatorStateInfo stateInfo, ECharacterState state)
    {
        return stateInfo.IsName(state.ToString());
    }

    public bool IsEndState(AnimatorStateInfo stateInfo)
    {
        return stateInfo.normalizedTime >= 1.0f;
    }
    #endregion
}
