using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParam { }

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