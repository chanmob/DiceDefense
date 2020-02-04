/*
  ============================================
	Author	: 김찬영
	Time 	: 2020-02-04 오후 4:32:08
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_GameExit : UI_Base
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
            case "Button_Yes":
                break;
            case "Button_No":
                break;
        }
    }
}