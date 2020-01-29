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
    [HideInInspector] public Panel_UnitAttribute panel_UnitAttribute;

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
        GetUiInstance(out panel_UnitAttribute);
	}
}