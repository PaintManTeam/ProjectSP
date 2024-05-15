using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Creature : BaseObject
{
    [SerializeField]
    private ECreatureState _creatureState = ECreatureState.None;
    public ECreatureState CreatureState
    {
        get { return _creatureState; }
        protected set
        {
            if (_creatureState == ECreatureState.Dead)
                return;

            if (_creatureState == value)
                return;

            bool isChangeState = true;
            switch(value)
            {
                case ECreatureState.Idle:
                    isChangeState = IdleStateCondition();
                    break;
                case ECreatureState.Move:
                    isChangeState = MoveStateCondition();
                    break;
                case ECreatureState.Jump:
                    isChangeState = JumpStateCondition();
                    break;
                case ECreatureState.FallDown:
                    isChangeState = FallDownStateCondition();
                    break;
                case ECreatureState.Climb:
                    isChangeState = ClimbStateCondition();
                    break;
                case ECreatureState.Interaction:
                    isChangeState = InteractionStateCondition();
                    break;
                case ECreatureState.Dead:
                    isChangeState = DeadStateCondition();
                    break;
            }

            if (isChangeState == false)
                return;

            _creatureState = value;
            PlayAnimation(value);

            switch (value)
            {
                case ECreatureState.Idle:
                    IdleStateOperate();
                    break;
                case ECreatureState.Move:
                    MoveStateOperate();
                    break;
                case ECreatureState.Jump:
                    JumpStateOperate();
                    break;
                case ECreatureState.FallDown:
                    FallDownStateOperate();
                    break;
                case ECreatureState.Climb:
                    ClimbStateOperate();
                    break;
                case ECreatureState.Interaction:
                    InteractionStateOperate();
                    break;
                case ECreatureState.Dead:
                    DeadStateOperate();
                    break;
            }
        }
    }

    public ECreatureType CreatureType { get; protected set; }

    [SerializeField] protected CreatureFoot creatureFoot;

    protected Rigidbody2D Rigid { get; private set; }
    public CapsuleCollider2D Collider { get; private set; }
    public float ColliderCenter { get { return Collider != null ? Collider.size.y / 2 : 0.0f; } }
    public Vector3 CenterPosition { get { return transform.position + Vector3.up * ColliderCenter; } }

    protected Animator animator;
    
    private bool _lookLeft = false;
    public bool LookLeft
    {
        get { return _lookLeft; }
        set
        {
            if(_lookLeft == value)
                return;

            _lookLeft = value;
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

        Collider = GetComponent<CapsuleCollider2D>();
        Rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        if(creatureFoot == null)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.name == "Foot")
                {
                    creatureFoot = child.GetComponent<CreatureFoot>();
                    break;
                }
            }
        }

        return true;
    }

    public virtual void SetInfo(int templateID)
    {
        CreatureState = ECreatureState.Idle;
    }

    public override Vector2 GetTopPosition()
    {
        return CenterPosition + Vector3.up * ColliderCenter;
    }

    #region Rigid
    protected void SetRigidVelocityX(float x)
    {
        Rigid.velocity = new Vector2(x, Rigid.velocityY);
    }

    protected void SetRigidVelocityY(float y)
    {
        Rigid.velocity = new Vector2(Rigid.velocityX, y);
    }

    protected void SetRigidVelocityZero()
    {
        Rigid.velocity = Vector2.zero;
    }
    #endregion

    #region State Condition
    protected virtual bool IdleStateCondition() { return true; }
    protected virtual bool MoveStateCondition() { return true; }
    protected virtual bool JumpStateCondition() { return true; }
    protected virtual bool FallDownStateCondition() { return true; }
    protected virtual bool ClimbStateCondition() { return true; }
    protected virtual bool InteractionStateCondition() { return true; }
    protected virtual bool DeadStateCondition() { return true; }
    #endregion

    #region State Operate
    protected virtual void IdleStateOperate() { }
    protected virtual void MoveStateOperate() { }
    protected virtual void JumpStateOperate() { }
    protected virtual void FallDownStateOperate() { }
    protected virtual void ClimbStateOperate() { }
    protected virtual void InteractionStateOperate() { }
    protected virtual void DeadStateOperate() { }
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
