/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-15 오후 5:39:15
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : UIManager<InGameUIManager>
{
	/* [PUBLIC VARIABLE]					*/

	[HideInInspector] public Panel_MainInGame panel_MainInGame;
    [HideInInspector] public Panel_Upgrade panel_Upgrade;
    [HideInInspector] public Panel_Quest panel_Quest;
    [HideInInspector] public Panel_Setting panel_Setting;
    [HideInInspector] public Popup_UnitInfo popup_UnitInfo;
    [HideInInspector] public Panel_Result panel_Result;
    [HideInInspector] public Popup_GameExit popup_GameExit;
    [HideInInspector] public Popup_Language popup_Language;

	/* [PROTECTED && PRIVATE VARIABLE]		*/

	/*----------------[PUBLIC METHOD]------------------------------*/


	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	protected override void Awake()
	{
		base.Awake();

		GetUiInstance(out panel_MainInGame);
        GetUiInstance(out panel_Upgrade);
        GetUiInstance(out panel_Quest);
        GetUiInstance(out panel_Setting);
        GetUiInstance(out popup_UnitInfo);
        GetUiInstance(out panel_Result);
        GetUiInstance(out popup_GameExit);
        GetUiInstance(out popup_Language);
    }
}