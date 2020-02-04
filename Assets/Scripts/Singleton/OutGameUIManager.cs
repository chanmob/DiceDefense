/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-02-04 오후 4:17:26
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutGameUIManager : UIManager<OutGameUIManager>
{
    /* [PUBLIC VARIABLE]					*/

    [HideInInspector] public Panel_MainOutGame panel_MainOutGame;
    [HideInInspector] public Panel_HowToPlay panel_HowToPlay;
    [HideInInspector] public Popup_GameExit popup_GameExit;

    /* [PROTECTED && PRIVATE VARIABLE]		*/


    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Awake()
    {
        base.Awake();

        GetUiInstance(out panel_MainOutGame);
        GetUiInstance(out panel_HowToPlay);
        GetUiInstance(out popup_GameExit);
    }
}