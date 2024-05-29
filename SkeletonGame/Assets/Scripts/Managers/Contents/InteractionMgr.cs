using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상호작용에 의한 기능 등을 총괄하는 매니저
/// </summary>
public class InteractionMgr
{

    public void Init()
    {

    }

    public void Clear()
    {

    }

    #region Dialogue
    
    // 데이터매니저에게 필요한 데이터를 추출해 큐에 담아 UI를 띄움
    // 이 때, 다이얼로그가 끝남을 알리는 콜백함수 연결

    public void ActiveDialogue(int startIndex, int endIndex, Action onEndDialogue)
    {
        Queue<DialogueData> dataQueue = new Queue<DialogueData>();

        for(int i = startIndex; i <= endIndex; i++)
        {
            dataQueue.Enqueue(Managers.Data.DialogueDict[i]);
        }

        UIDialogueParam param = new UIDialogueParam(onEndDialogue, dataQueue);
        Managers.UI.OpenPopupUI<UI_DialoguePopup>(param);
    }

    #endregion

    #region Portal

    // 오브젝트 매니저를 통해 이동할 포탈 고유 Id를 가져와 타겟을 넘겨줌?

    #endregion
}
