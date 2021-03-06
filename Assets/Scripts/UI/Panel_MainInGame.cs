﻿/*
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
    public Text text_time;
    public Text text_monsterCount;
    public Text text_round;

    public Image image_timeCheck;
    public Image image_monsterFill;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private Text _text_Speed;

    private InGameManager _ingameManager;
	private UnitManager _unitManager;

    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Start()
    {
        base.Start();

        if(_ingameManager == null)
        {
            _ingameManager = InGameManager.instance;
        }

		if(_unitManager == null)
		{
			_unitManager = UnitManager.instance;
		}

        text_gold.text = LanguageManager.instance.GetCurrentLanguageText("Text_Gold") + " : " + _ingameManager.gold.ToString();
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
                int cost = 30;
                int len = _ingameManager.spawnIndex.Count;

                if (_ingameManager.CheckGold(cost) && len > 0)
                {
                    _ingameManager.GetGold(-cost);
                    _unitManager.BuyUnit();
                }
                break;
            case "Button_Setting":
                InGameUIManager.instance.panel_Setting.Show();
                break;
            case "Button_GameSpeed":
                SetGameSpeed();
                break;
            case "Button_AutoLevelUp":
                UnitManager.instance.AutoUnitLevelUp();
                break;
        }
    }

    private void SetGameSpeed()
    {
        int speed = (int)Time.timeScale;

        switch (speed)
        {
            case 1:
                Time.timeScale = 2;
                _text_Speed.text = "x2.0"; 
                break;
            case 2:
                Time.timeScale = 4;
                _text_Speed.text = "x4.0";
                break;
            case 4:
                Time.timeScale = 1;
                _text_Speed.text = "x1.0";
                break;
        }
    }
}