using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueComponent : GimmickInteractionComponent
{
    // 임시
    [SerializeField] int startIndex = 1;
    [SerializeField] int endIndex = 3;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        InteractionType = Define.EInteractionType.Dialogue;

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);


    }

    public override bool Interact(InteractionParam param = null)
    {
        if (base.Interact(param) == false)
            return false;

        if (param is InteractionDialogueParam dialogueParam)
        {
            Managers.Interaction.ActiveDialogue(startIndex, endIndex, dialogueParam.onEndDialogue);
        }

        return true;
    }
}
