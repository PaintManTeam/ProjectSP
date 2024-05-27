using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DialoguePopup : UI_BasePopup
{
    [SerializeField] Image speakerImage;
    [SerializeField] Text dialogueText;

    /* 다이얼로그 기능
    0. 오브젝트 활성화되기 직전에 출력할 대화에 필요한 정보 그룹을 Queue에 담아야 함 (SetInfo)

    1. 다이얼로그 그룹 하나는 자동으로 텍스트 완성이 진행
    - 이 때, 넘어가는 키 입력이 발생하면 완성된 텍스트로 넘어감

    2. 완성된 텍스트가 됐을 때 넘어가는 키 입력 대기
    - 입력이 되면 다음 다이얼로그 그룹을 출력
    
    3. 마지막 다이얼로그 그룹이라면, 창을 종료하고 대화가 완료됨을 반환해야 함
    - 완료 반환을 위해 다이얼로그 윈도우 규칙을 정해야 함 (다이얼로그 매니저?)
    */

    Action onEndDialogue = null;
    Queue<DialogueData> dataQueue;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public override void SetInfo(UIParam param)
    {
        base.SetInfo(param);

        if (param is UIDialogueParam dialogueParam)
        {
            // Queue에 출력할 데이터들을 담음

            onEndDialogue = dialogueParam.onEndDialogue;
            dataQueue = dialogueParam.dataQueue;
        }
    }

    public override void OpenPopupUI()
    {
        SetDialogueInfo();

        base.OpenPopupUI();

        StartDialogue();
    }

    private void SetDialogueInfo()
    {
        // 다이얼로그 활성화 전 초기 세팅
    }

    private void StartDialogue()
    {
        ConnectInputActions(true);

        // 다이얼로그 코루틴 실행
    }

    private void EndDialogue()
    {
        ConnectInputActions(false);
        ClosePopupUI();
        onEndDialogue?.Invoke();
    }

    #region Input
    private void ConnectInputActions(bool isConnect)
    {
        Managers.Input.OnSpaceKeyEntered -= OnNextDialogueKey;

        if(isConnect)
        {
            Managers.Input.OnSpaceKeyEntered += OnNextDialogueKey;
        }
    }
    public void OnNextDialogueKey()
    {
        Debug.Log($"{this.gameObject.activeSelf} : 다음 다이얼로그 키 입력받음");
    }
    public void OnSkipKey()
    {
        // 아직 미구현
    }
    #endregion
}
