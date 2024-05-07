using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Define;

public class Player : Character
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

    private void Start()
    {
        // 임시
        IsPlayerInputControll = true;
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public override void SetInfo()
    {
        base.SetInfo();

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

            yield return new WaitForFixedUpdate();
        }

        SetRigidVelocityZero();
        coPlayerInputController = null;
    }

    public void OnMove(InputValue value)
    {
        if (!IsPlayerInputControll)
            return;

        moveDirection = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (!IsPlayerInputControll)
            return;

        Debug.Log("점프 키 입력");

        // 점프가 가능한 상태인지 체크

        // 가능하다면 중력 초기화 후 점프 (2단점프 고려)

        SetRigidVelocityY(JumpPower);
    }

    public void OnInteraction()
    {
        if (!IsPlayerInputControll)
            return;

        Debug.Log("상호작용 키 입력");

        
    }
    #endregion
}
