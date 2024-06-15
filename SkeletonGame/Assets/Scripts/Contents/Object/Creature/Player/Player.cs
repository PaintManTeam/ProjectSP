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
    [SerializeField] private bool _isPlayerInputControll = false;
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
            else if(_isPlayerInputControll == false && CreatureState != ECreatureState.Dead)
            {
                // 강제로 모션 변환
                PlayAnimation(ECreatureState.Idle);
                IdleStateOperate();
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

        InteractTarget();
    }
    #endregion

    #region CreatureState Controll

    Coroutine coPlayerStateController = null;
    protected IEnumerator CoPlayerStateController()
    {
        while (IsPlayerInputControll)
        {
            switch (CreatureState)
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
                case ECreatureState.EnterPortal:
                    UpdateEnterPortal();
                    break;
                case ECreatureState.ComeOutPortal:
                    UpdateComeOutPortal();
                    break;
            }

            yield return null;
        }

        coPlayerStateController = null;
    }

    #region Idle Motion
    protected override bool IdleStateCondition()
    {
        if (base.IdleStateCondition() == false)
            return false;

        if (Rigid.velocity != Vector2.zero)
            return false;

        if (creatureFoot.IsLandingGround == false)
            return false;

        return true;
    }

    private void UpdateIdle()
    {
        FallDownCheck();
    }

    protected override void IdleStateOperate()
    {
        base.IdleStateOperate();

        SetRigidVelocityZero();
    }
    #endregion

    #region Move Motion
    protected override bool MoveStateCondition()
    {
        if (base.MoveStateCondition() == false)
            return false;

        if (moveDirection.x == 0)
            return false;

        if (creatureFoot.IsLandingGround == false)
            return false;

        return true;
    }

    private void UpdateMove()
    {
        FallDownCheck();
        MovementCheck();

        if (moveDirection.x == 0)
            CreatureState = ECreatureState.Idle;
    }

    protected override void MoveStateOperate()
    {
        base.MoveStateOperate();


    }

    private void MovementCheck()
    {
        SetRigidVelocityX(moveDirection.x * Speed);

        if (moveDirection.x > 0)
            LookLeft = false;
        else if (moveDirection.x < 0)
            LookLeft = true;
    }
    #endregion

    #region Jump Motion
    protected override bool JumpStateCondition()
    {
        if (base.JumpStateCondition() == false)
            return false;

        if (creatureFoot.IsLandingGround == false)
            return false;

        return true;
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

    protected override void JumpStateOperate()
    {
        base.JumpStateOperate();

        SetRigidVelocityY(JumpPower);
    }
    #endregion

    #region FallDown Motion
    protected override bool FallDownStateCondition()
    {
        if (base.FallDownStateCondition() == false)
            return false;

        if (Rigid.velocityY >= 0)
            return false;

        return true;
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

    protected override void FallDownStateOperate()
    {
        base.FallDownStateOperate();


    }

    private void FallDownCheck()
    {
        if (creatureFoot.IsLandingGround == false && Rigid.velocityY < 0)
            CreatureState = ECreatureState.FallDown;
    }
    #endregion

    #region Climb Motion
    protected override bool ClimbStateCondition()
    {
        if (base.ClimbStateCondition() == false)
            return false;

        // 오르거나 내릴 수 있는 사다리가 범위 내에 있는지 확인

        return true;
    }

    private void UpdateClimb()
    {
        // 사다리 끝에 도달했는지 확인

        // 위아래 이동

        // 좌우이동은 제한
    }

    protected override void ClimbStateOperate()
    {
        base.ClimbStateOperate();

    }
    #endregion

    #region Interaction Motion
    protected override bool InteractionStateCondition()
    {
        if (base.InteractionStateCondition() == false)
            return false;

        if (creatureFoot.IsLandingGround == false)
            return false;

        if (interactionTarget == null)
            return false;

        if (interactionTarget.IsInteractable() == false)
            return false;

        return true;
    }

    private void UpdateInteraction()
    {
        // 모션 끊기는 조건 확인

        // 애니메이션 종료 확인
        if (IsEndCurrentState(ECreatureState.Interaction))
        {
            if(interactionTarget != null)
            {
                interactionTarget.Interact();
                interactionRange.Interactioncomplete(interactionTarget);
                interactionTarget = null;
            }

            CreatureState = ECreatureState.Move;
            CreatureState = ECreatureState.Idle;
        }
    }

    protected override void InteractionStateOperate()
    {
        base.InteractionStateOperate();
    }

    IInteraction interactionTarget = null;
    public void OnDetectInteractionObject(IInteraction interactionTarget)
    {
        this.interactionTarget = interactionTarget;
    }

    public void InteractTarget()
    {
        if (creatureFoot.IsLandingGround == false)
            return;

        if (interactionTarget == null)
            return;

        // 상호작용 방향 세팅
        SetRigidVelocityZero();
        float dirX = transform.position.x - interactionTarget.WorldPosition.x;
        LookLeft = dirX > 0;

        // 상호작용 물체에 따른 동작
        switch (interactionTarget.InteractionType)
        {
            case EInteractionType.EndMotion:
                CreatureState = ECreatureState.Interaction;
                break;
            case EInteractionType.Dialogue:
                InteractDialogue();
                break;
            case EInteractionType.Portal:
                InteractPortal();
                break;
        }
    }

    private void InteractDialogue()
    {
        CreatureState = ECreatureState.Idle;
        IsPlayerInputControll = false;

        InteractionDialogueParam param = new InteractionDialogueParam(OnEndDialogue);
        interactionTarget.Interact(param);
    }

    private void InteractPortal()
    {
        InteractionPortalParam param = new InteractionPortalParam(OnTeleportTarget);
        interactionTarget.Interact(param);
    }

    public void OnEndDialogue()
    {
        IsPlayerInputControll = true;
    }
    #endregion

    #region EnterPortal Motion
    protected override bool EnterPortalStateCondition()
    {
        if (base.EnterPortalStateCondition() == false)
            return false;

        if (teleportTarget == null)
            return false;

        return true;
    }

    private void UpdateEnterPortal()
    {
        Color tempColor = SpriteRender.color;

        if (tempColor.a > 0.05f)
        {
            tempColor.a -= 2.0f * Time.deltaTime; // 시간 조절
            SpriteRender.color = tempColor;
        }
        else
        {
            tempColor.a = 0.0f;
            SpriteRender.color = tempColor;
            isWaitFadeInEffect = true;
            CreatureState = ECreatureState.ComeOutPortal;

            if (CreatureState != ECreatureState.ComeOutPortal)
                return;

            UIFadeEffectParam param = new UIFadeEffectParam(
                fadeInEffectCondition: () => CreatureState == ECreatureState.ComeOutPortal,
                onFadeOutCallBack: OnTeleportFadeOut,
                onFadeInCallBack: OnTeleportFadeIn);
            Managers.UI.OpenPopupUI<UI_FadeEffectPopup>(param);
        }
    }

    protected override void EnterPortalStateOperate()
    {
        base.EnterPortalStateOperate();

        if (interactionTarget is PortalObject portalObejct)
        {
            SetRigidVelocityZero();

            // 순간이동(임시)
            this.transform.position = portalObejct.GetBottomPosition();
        }

        interactionTarget = null; // 상호작용 중인 타겟 제거
        ConnectInputActions(false); // 키 입력 제한
    }

    public void OnTeleportFadeOut()
    {
        // 포탈에 타고 페이드 아웃 - 페이드 인 사이에 실행
        if (teleportTarget != null)
            this.transform.position = teleportTarget.GetBottomPosition();
    }
    bool isWaitFadeInEffect = true; // 임시 (맘에 안듦)
    public void OnTeleportFadeIn()
    {
        isWaitFadeInEffect = false;
    }

    BaseObject teleportTarget = null;
    public void OnTeleportTarget(BaseObject teleportTarget)
    {
        if (teleportTarget == null)
            Debug.LogWarning("teleportTarget is Null!");

        this.teleportTarget = teleportTarget;
        CreatureState = ECreatureState.EnterPortal;
    }
    #endregion

    #region ComeOutPortal Motion
    protected override bool ComeOutPortalStateCondition()
    {
        if (base.ComeOutPortalStateCondition() == false)
            return false;

        return true;
    }

    private void UpdateComeOutPortal()
    {
        if (isWaitFadeInEffect)
            return;

        Color tempColor = SpriteRender.color;

        if (tempColor.a < 0.95f)
        {
            tempColor.a += Time.deltaTime / 1f; // 페이드 시간 조절
            SpriteRender.color = tempColor;
        }
        else
        {
            tempColor.a = 1.0f;
            SpriteRender.color = tempColor;
            CreatureState = ECreatureState.Idle;
            ConnectInputActions(true);
        }
    }

    protected override void ComeOutPortalStateOperate()
    {
        base.ComeOutPortalStateOperate();

    }
    #endregion

    #region Dead Motion
    protected override bool DeadStateCondition()
    {
        if (base.InteractionStateCondition() == false)
            return false;

        // 캐릭터가 죽는 조건 체크

        return true;
    }

    protected override void DeadStateOperate()
    {
        base.DeadStateOperate();


    }
    #endregion
    
    #endregion
}
