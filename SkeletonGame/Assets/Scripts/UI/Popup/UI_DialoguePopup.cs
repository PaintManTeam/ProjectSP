using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
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
        // 다이얼로그 활성화 전 기본 값 세팅
        dialogueText.text = "";
    }

    private void StartDialogue()
    {
        ConnectInputActions(true);

        coDialogueSequenceExecuter = StartCoroutine(CoDialogueSequenceExecuter(0.1f));
    }

    private void EndDialogue()
    {
        ConnectInputActions(false);
        ClosePopupUI();
        onEndDialogue?.Invoke();
    }

    Coroutine coDialogueSequenceExecuter = null;
    private IEnumerator CoDialogueSequenceExecuter(float typingDelayTime)
    {
        while(dataQueue.Count > 0)
        {
            dialogueText.text = "";
            DialogueData data = dataQueue.Dequeue();

            if (coDialogueTyping != null)
            {
                StopCoroutine(coDialogueTyping);
                coDialogueTyping = null;
            }

            // 타이핑 기능
            coDialogueTyping = StartCoroutine(CoDialogueTyping(data.Dialogue, 0.1f)); // 시간 조절

            // 타이핑이 끝날 때까지 대기
            yield return new WaitUntil(() => coDialogueTyping == null);
            dialogueText.text = data.Dialogue;

            // 스킵 키가 눌릴 때까지 대기
            yield return new WaitUntil(() => isSkip);
            isSkip = false;
        }
        
        EndDialogue();
        coDialogueSequenceExecuter = null;
    }

    Coroutine coDialogueTyping = null;
    private IEnumerator CoDialogueTyping(string dialogue, float typingDelayTime)
    {
        int index = 0;

        while(index < dialogue.Length)
        {
            dialogueText.text += dialogue[index];
            yield return new WaitForSeconds(typingDelayTime);
            index++;
        }

        dialogueText.text = dialogue;
        coDialogueTyping = null;
    }

    #region Input
    private void ConnectInputActions(bool isConnect)
    {
        Managers.Input.OnSpaceKeyEntered -= OnSkipKey;

        if(isConnect)
        {
            Managers.Input.OnSpaceKeyEntered += OnSkipKey;
        }
    }

    private bool isSkip = false;
    public void OnSkipKey()
    {
        if(coDialogueTyping != null)
        {
            StopCoroutine(coDialogueTyping);
            coDialogueTyping = null;
            return;
        }

        if(coDialogueSequenceExecuter != null)
        {
            isSkip = true;
            return;
        }
    }
    #endregion
}
