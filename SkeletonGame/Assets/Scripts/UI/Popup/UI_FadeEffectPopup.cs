using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeEffectPopup : UI_BasePopup
{
    [SerializeField] private Image fadeEffectImage;

    private Coroutine fadeEffectCoroutine = null;
    private Func<bool> fadeInEffectCondition = null;
    private Action fadeOutInCallBack = null;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public override void OpenPopupUI()
    {
        base.OpenPopupUI();

        if (fadeEffectCoroutine == null)
        {
            fadeEffectCoroutine = StartCoroutine(IfadeOutInEffect(0.5f));
        }
        else
            Debug.Log("FadeEffect 중복");
    }

    public override void SetInfo(UIParam param)
    {
        base.SetInfo(param);

        if (param is UIFadeEffectParam)
        {
            var fadeEffectParam = (UIFadeEffectParam)param;
            
            this.fadeInEffectCondition = fadeEffectParam.fadeInEffectCondition;
            this.fadeOutInCallBack = fadeEffectParam.fadeOutInCallBack;
        }
    }

    public override void ClosePopupUI()
    {
        base.ClosePopupUI();

        fadeInEffectCondition = null;
        fadeOutInCallBack = null;
    }

    public override void DeActivePopup()
    {
        if (fadeEffectCoroutine != null)
            return;

        base.DeActivePopup();
    }

    private IEnumerator IfadeOutInEffect(float fadeTime)
    {
        // FadeOut Effect
        fadeEffectImage.color = new Color(0, 0, 0, 0);
        Color tempColor = fadeEffectImage.color;

        while (tempColor.a < 0.99f)
        {
            tempColor.a += Time.deltaTime / fadeTime;
            fadeEffectImage.color = tempColor;

            yield return null;
        }

        tempColor.a = 1f;
        fadeEffectImage.color = tempColor;

        // Wait Condition
        fadeOutInCallBack?.Invoke();

        if (fadeInEffectCondition != null)
        {
            var loadingPopup = Managers.UI.OpenPopupUI<UI_LoadingPopup>();
            yield return new WaitUntil(fadeInEffectCondition);
            Managers.UI.ClosePopupUI(loadingPopup);
        }

        // FadeIn Effect
        while (tempColor.a > 0.01f)
        {
            tempColor.a -= Time.deltaTime / fadeTime;
            fadeEffectImage.color = tempColor;

            if (tempColor.a <= 0f) tempColor.a = 0f;

            yield return null;
        }

        fadeEffectImage.color = tempColor;
        fadeEffectCoroutine = null;
        ClosePopupUI();
    }
}