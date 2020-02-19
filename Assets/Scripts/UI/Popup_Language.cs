/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-02-18 오후 5:40:16
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Popup_Language : UI_Base
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/


    /*----------------[PUBLIC METHOD]------------------------------*/

    public void Open()
    {
        Show();
        transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private void Close()
    {
        transform.DOScale(0, 0.25f).SetEase(Ease.InQuad).OnComplete(() => Hide());
    }

    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "Button_Korean":
                LanguageManager.instance.SetLangauge(SystemLanguage.Korean);
                break;
            case "Button_English":
                LanguageManager.instance.SetLangauge(SystemLanguage.English);
                break;
            case "Button_Close":
                Close();
                break;
        }
    }
}