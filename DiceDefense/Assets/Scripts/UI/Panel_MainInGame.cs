/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-15 오후 5:40:47
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_MainInGame : UI_Base
{
	/* [PUBLIC VARIABLE]					*/

	public Text text_gold;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private InGameManager _ingameManager;

    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Start()
    {
        base.Start();

        if(_ingameManager == null)
        {
            _ingameManager = InGameManager.instance;
        }
    }

    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "Button_Upgrade":
                InGameUIManager.instance.panel_Upgrade.Show();
                break;
            case "Button_Quest":
                InGameUIManager.instance.panel_Quest.Show();
                break;
            case "Button_BuyUnit":
                _ingameManager.BuyUnit();
                break;
            case "Button_Setting":
                InGameUIManager.instance.panel_Setting.Show();
                break;
            case "Button_GameSpeed":
                break;
        }
    }
}