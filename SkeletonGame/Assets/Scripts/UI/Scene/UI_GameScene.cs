using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameScene : UI_BaseScene
{
    [SerializeField] GameObject DialoguePopup;
    [SerializeField] Text text;

    string[] strings = new string[4]
    {
        "까꿍 ㅋㅋㅋ", "저는 해골입니다.", "안녕하세요 페인트맨 여러분", "사람이 되고시펑"
    };

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public void OnClickTest()
    {
        UIFadeEffectParam param = new UIFadeEffectParam(null, A);
        Managers.UI.OpenPopupUI<UI_FadeEffectPopup>(param);
    }

    public void A()
    {
        int index = Random.Range(0, strings.Length);

        text.text = strings[index];

        StartCoroutine(IWaitActive(1f));
    }

    private IEnumerator IWaitActive(float time)
    {
        DialoguePopup.gameObject.SetActive(true);

        yield return new WaitForSeconds(time);

        DialoguePopup.gameObject.SetActive(false);
    }
}
