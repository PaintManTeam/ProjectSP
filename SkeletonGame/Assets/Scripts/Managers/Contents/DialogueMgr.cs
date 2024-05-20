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
}
