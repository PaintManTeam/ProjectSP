using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static Define;

public class Player : Creature
{
    protected PlayerInteractionRange interactionRange;

    // 플레이어를 조작할 수 있는 경우
    private bool _isPlayerInputControll = false;
    public bool IsPlayerInputControll
    {
        get { return _isPlayerInputControll; }
        protected set
        {
            if (_isPlayerInputControll == value)
                return;

            _isPlayerInputControll = value;
            ConnectInputActions(value);

            if (_isPlayerInputControll && coPlayerStateController == null)
            {
                coPlayerStateController = StartCoroutine(CoPlayerStateController());
            }
            else if (!_isPlayerInputControll)
            {
                StopCoroutine(coPlayerStateController);
            }
        }
    }

    // 플레이어 데이터
    protected float Speed = 3.0f;
    protected float JumpPower = 5.0f;
    protected float InteractionRangeRadius = 0.5f;

    // 공통 데이터
    protected float MarginalSpeed = 10.0f;

    private void Start()
    {
        // 임시
        IsPlayerInputControll = true; // 게임 매니저에서 할 것
        SetInfo(0);
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        interactionRange ??= Util.FindChild<PlayerInteractionRange>(gameObject);
        
        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        CreatureType = ECreatureType.Player;

        Camera.main.GetOrAddComponent<CameraController>().Target = this;

        interactionRange.SetInfo(OnDetectInteractionObject, ColliderCenter, InteractionRangeRadius);
    }

    #region Interaction
    [SerializeField] InteractionObject interactionObject = null;
    public void OnDetectInteractionObject(InteractionObject interactionObject)
    {
        this.interactionObject = interactionObject;
    }

    // 상호작용 가능 키 띄우기
    #endregion

    #region Input
    private Vector2 moveDirection = Vector2.zero;

    private void ConnectInputActions(bool isConnect)
    {
        Managers.Input.OnArrowKeyEntered -= OnArrowKey;
        Managers.Input.OnSpaceKeyEntered -= OnJumpKey;
        Managers.Input.OnEKeyEntered -= OnInteractionKey;

        if (isConnect)
        {
            Managers.Input.OnArrowKeyEntered += OnArrowKey;
            Managers.Input.OnSpaceKeyEntered += OnJumpKey;
            Managers.Input.OnEKeyEntered += OnInteractionKey;
        }
        else
        {
            if (coPlayerStateController != null)
            {
                StopCoroutine(coPlayerStateController);
                coPlayerStateController = null;
            }
        }
    }

    public void OnArrowKey(Vector2 value)
    {
        if (!IsPlayerInputControll)
            return;

        moveDirection = value;

        if (moveDirection.x != 0)
            CreatureState = ECreatureState.Move;
    }

    public void OnJumpKey()
    {
        if (!IsPlayerInputControll)
            return;

        CreatureState = ECreatureState.Jump;
    }

    public void OnInteractionKey()
    {
        if (!IsPlayerInputControll)
            return;

        CreatureState = ECreatureState.Interaction;
    }
    #endregion

    #region State Condition
    protected override bool IdleStateCondition()
    {
        if (base.IdleStateCondition() == false)
            return false;

        if (Rigid.velocity != Vector2.zero)
            return false;

        return true;
    }

    protected override bool MoveStateCondition()
    {
        if (base.MoveStateCondition() == false)
            return false;

        if (creatureFoot.IsLandingGround == false)
            return false;

        return true;
    }

    protected override bool JumpStateCondition()
    {
        if (base.JumpStateCondition() == false)
            return false;

        if (creatureFoot.IsLandingGround == false)
            return false;

        return true;
    }

    protected override bool FallDownStateCondition()
    {
        if (base.FallDownStateCondition() == false)
            return false;

        if (Rigid.velocityY >= 0)
            return false;

        return true;
    }

    protected override bool ClimbStateCondition()
    {
        if (base.ClimbStateCondition() == false)
            return false;

        // 오르거나 내릴 수 있는 사다리가 범위 내에 있는지 확인

        return true;
    }

    protected override bool InteractionStateCondition()
    {
        if (base.InteractionStateCondition() == false)
            return false;

        if (creatureFoot.IsLandingGround == false)
            return false;

        if (interactionObject == null || interactionObject.GimmickState != EGimmickObjectState.Ready)
            return false;
        
        return true;
    }

    protected override bool DeadStateCondition()
    {
        if (base.InteractionStateCondition() == false)
            return false;

        // 캐릭터가 죽는 조건 체크

        return true;
    }
    #endregion
   
    #region State Controll
    Coroutine coPlayerStateController = null;
    protected IEnumerator CoPlayerStateController()
    {
        yield return null;

        while(IsPlayerInputControll)
        {
            switch(CreatureState)
            {
                case ECreatureState.Idle:
                    UpdateIdle();
                    break;
                case ECreatureState.Move:
                    UpdateMove();
                    break;
                case ECreatureState.Jump:
                    UpdateJump();
                    break;
                case ECreatureState.FallDown:
                    UpdateFallDown();
                    break;
                case ECreatureState.Climb:
                    UpdateClimb();
                    break;
                case ECreatureState.Interaction:
                    UpdateInteraction();
                    break;
                case ECreatureState.Dead:
                    StopCoroutine(coPlayerStateController);
                    coPlayerStateController = null;
                    break;
            }

            yield return null;
        }

        coPlayerStateController = null;
    }

    private void UpdateIdle()
    {
        FallDownCheck();
    }

    private void UpdateMove()
    {
        FallDownCheck();
        MovementCheck();

        if (moveDirection.x == 0)
            CreatureState = ECreatureState.Idle;
    }

    private void UpdateJump()
    {
        // 착지 확인
        if (creatureFoot.IsLandingGround)
        {
            CreatureState = ECreatureState.Move;
            CreatureState = ECreatureState.Idle;
            return;
        }

        FallDownCheck();
        MovementCheck();
    }

    private void UpdateFallDown()
    {
        MovementCheck();

        // 낙하 속도 제한
        if (Rigid.velocityY < MarginalSpeed * -1.0f)
            SetRigidVelocityY(MarginalSpeed * -1.0f);

        // 착지 확인
        if (creatureFoot.IsLandingGround)
        {
            CreatureState = ECreatureState.Move;
            CreatureState = ECreatureState.Idle;
        }
    }

    private void UpdateClimb()
    {
        // 사다리 끝에 도달했는지 확인

        // 위아래 이동
    }

    private void UpdateInteraction()
    {
        // 모션 끊기는 조건 확인


        // 애니메이션 종료 확인
        if(IsEndCurrentState(ECreatureState.Interaction))
        {
            if (interactionObject != null)
                interactionObject.Interact();

            CreatureState = ECreatureState.Move;
            CreatureState = ECreatureState.Idle;
        }
    }

    private void MovementCheck()
    {
        SetRigidVelocityX(moveDirection.x * Speed);

        if (moveDirection.x > 0)
            LookLeft = false;
        else if (moveDirection.x < 0)
            LookLeft = true;
    }

    private void FallDownCheck()
    {
        if(creatureFoot.IsLandingGround == false && Rigid.velocityY < 0)
            CreatureState = ECreatureState.FallDown;
    }
    #endregion

    #region State Operate
    protected override void IdleStateOperate()
    {
        base.IdleStateOperate();


    }

    protected override void MoveStateOperate()
    {
        base.MoveStateOperate();


    }

    protected override void JumpStateOperate()
    {
        base.JumpStateOperate();

        SetRigidVelocityY(JumpPower);
    }

    protected override void FallDownStateOperate()
    {
        base.FallDownStateOperate();


    }

    protected override void ClimbStateOperate()
    {

        base.ClimbStateOperate();

    }

    protected override void InteractionStateOperate()
    {
        base.InteractionStateOperate();

        SetRigidVelocityZero();
    }

    protected override void DeadStateOperate()
    {
        base.DeadStateOperate();


    }
    #endregion
}
