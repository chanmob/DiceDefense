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

public class InGameUIManager : UIManager
{
	/* [PUBLIC VARIABLE]					*/

	[HideInInspector] public Panel_MainInGame panel_mainInGame;

	/* [PROTECTED && PRIVATE VARIABLE]		*/

	/*----------------[PUBLIC METHOD]------------------------------*/


	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	protected override void Awake()
	{
		base.Awake();

		GetUiInstance(out panel_mainInGame);
	}
}