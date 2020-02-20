/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-02-04 오후 4:32:08
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Popup_GameExit : UI_Base
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private GameObject _childPanel;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void Open()
    {
        Time.timeScale = 0;
        Show();
        _childPanel.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private void Close()
    {
        _childPanel.transform.DOScale(0, 0.25f).SetEase(Ease.InQuad).OnComplete(() => 
        {
            Hide();
            Time.timeScale = 1;
        });
    }

    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "Button_Yes":
                Application.Quit();
                break;
            case "Button_No":
                Close();
                break;
        }
    }
}