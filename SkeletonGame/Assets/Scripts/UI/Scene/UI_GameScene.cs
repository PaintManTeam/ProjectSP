using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameScene : UI_BaseScene
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public void OnClickTest()
    {
        Managers.UI.OpenPopupUI<UI_FadeEffectPopup>();
    }
}