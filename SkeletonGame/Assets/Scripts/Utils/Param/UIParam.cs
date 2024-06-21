using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParam { }

public class UIDialogueParam : UIParam
{
    public Action onEndDialogue;
    public Queue<JDialogueData> dataQueue;

    public UIDialogueParam(Action onEndDialogue, Queue<JDialogueData> dataQueue)
    {
        this.onEndDialogue = onEndDialogue;
        this.dataQueue = dataQueue;
    }
}

public class UILoadingParam : UIParam
{
    
}

public class UIFadeEffectParam : UIParam
{
    public Func<bool> fadeInEffectCondition;
    public Action onFadeOutCallBack;
    public Action onFadeInCallBack;

    public UIFadeEffectParam(Func<bool> fadeInEffectCondition = null, Action onFadeOutCallBack = null, Action onFadeInCallBack = null)
    {
        this.fadeInEffectCondition = fadeInEffectCondition;
        this.onFadeOutCallBack = onFadeOutCallBack;
        this.onFadeInCallBack = onFadeInCallBack;
    }  
}