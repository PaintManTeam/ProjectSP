using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParam { }

public class DialogueParam : UIParam
{
    // 다이얼로그 매니저가 UI매니저한테 요청할 정보가 되야 할 듯 ?
}

public class UILoadingParam : UIParam
{
    
}

public class UIFadeEffectParam : UIParam
{
    public Func<bool> fadeInEffectCondition;
    public Action fadeOutInCallBack;

    public UIFadeEffectParam(Func<bool> fadeInEffectCondition, Action fadeOutInCallBack = null)
    {
        this.fadeInEffectCondition = fadeInEffectCondition;
        this.fadeOutInCallBack = fadeOutInCallBack;
    }  
}