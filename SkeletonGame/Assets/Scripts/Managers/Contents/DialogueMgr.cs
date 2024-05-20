using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueMgr
{
    UI_DialogueWindow dialogueWindow;

    public void Init()
    {
        dialogueWindow = Managers.UI.SceneUI.GetComponent<UI_GameScene>().dialogueWindow;
        dialogueWindow.Init();
    }

    public void Clear()
    {

    }

    // 다이얼로그를 띄워달라는 명령 전달?
}
