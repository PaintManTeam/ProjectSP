using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static Define;

public class Player : Creature
{
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

            if (_isPlayerInputControll && coPlayerInputController == null)
            {
                coPlayerInputController = StartCoroutine(CoPlayerInputController());
            }
            else if (!_isPlayerInputControll)
            {
                StopCoroutine(coPlayerInputController);
            }
        }
    }

    public  Vector2 moveDirection = Vector2.zero;

    private Coroutine coPlayerInputController = null;

    // 임시 (데이터로 뺄 것)
    protected float Speed = 3.0f;
    protected float JumpPower = 5.0f;


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

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        CreatureType = ECreatureType.Player;

        Camera.main.GetOrAddComponent<CameraController>().Target = this;

        
    }

    private void ConnectInputActions(bool isConnect)
    {
        Managers.Input.OnArrowKeyEntered -= OnMove;
        Managers.Input.OnSpaceKeyEntered -= OnJump;
        Managers.Input.OnEKeyEntered -= OnInteraction;

        if (isConnect)
        {
            Managers.Input.OnArrowKeyEntered += OnMove;
            Managers.Input.OnSpaceKeyEntered += OnJump;
            Managers.Input.OnEKeyEntered += OnInteraction;
        }
    }

    #region Input
    private IEnumerator CoPlayerInputController()
    {
        while (_isPlayerInputControll)
        {
            if(moveDirection.y != 0)
            {
                // 사다리가 있는지 확인해서 사다리를 오르거나 내림
            }

            SetRigidVelocityX(moveDirection.x * Speed);

            CreatureState = (moveDirection.x != 0) ? ECreatureState.Move : ECreatureState.Idle;

            if (moveDirection.x > 0)
                LookLeft = false;
            else if (moveDirection.x < 0)
                LookLeft = true;

            yield return new WaitForFixedUpdate();
        }

        SetRigidVelocityZero();
        coPlayerInputController = null;
    }

    public void OnMove(Vector2 value)
    {
        if (!IsPlayerInputControll)
            return;

        moveDirection = value;
    }

    public void OnJump()
    {
        if (!IsPlayerInputControll)
            return;

        SetRigidVelocityY(JumpPower);
    }

    public void OnInteraction()
    {
        if (!IsPlayerInputControll)
            return;

        Debug.Log("상호작용 키 입력");

        
    }
    #endregion

    #region State
    protected override bool IdleStateCondition()
    {

        
        return true;
    }

    protected override bool MoveStateCondition()
    {


        return true;
    }

    protected override bool JumpStateCondition()
    {


        return true;
    }

    protected override bool FallDownStateCondition()
    {


        return true;
    }

    protected override bool ClimbStateCondition()
    {


        return true;
    }

    protected override bool InteractionStateCondition()
    {


        return true;
    }

    protected override bool DeadStateCondition()
    {


        return true;
    }
    #endregion
}
