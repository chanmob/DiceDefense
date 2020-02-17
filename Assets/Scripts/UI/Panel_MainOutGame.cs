/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-02-04 오후 4:22:19
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_MainOutGame : UI_Base
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/


    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "Button_GamePlay":
                SceneControl.instance.SceneLoad("InGame");
                break;
            case "Button_HowToPlay":
                OutGameUIManager.instance.panel_HowToPlay.Show();
                break;
            case "Button_Exit":
                OutGameUIManager.instance.popup_GameExit.Show();
                break;
            case "Button_Store":
                Application.OpenURL("");
                break;
            case "Button_Sound":
                SoundManager.instance.VolumeOnOff();
                break;
            case "Button_Ranking":
                break;
            case "Button_Archieve":
                break;
        }
    }
}