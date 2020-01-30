/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-15 오후 11:02:47
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Upgrade : UI_Base
{
    /* [PUBLIC VARIABLE]					*/

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private Text[] _text_LevelTexts;
    [SerializeField] private Text[] _text_CostTexts;

    private InGameManager _ingameManager;

    private Animator _animator;

    /*----------------[PUBLIC METHOD]------------------------------*/

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
    }


    protected override void Start()
    {
        base.Start();

        if (_ingameManager == null)
        {
            _ingameManager = InGameManager.instance;
        }
    }

    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "Button_Upgrade1":
                if(_ingameManager.CheckGold(_ingameManager.cost_Upgrade1))
                {
                    _ingameManager.GetGold(_ingameManager.cost_Upgrade1);
                    _ingameManager.amount_Upgrade1++;
                    _ingameManager.cost_Upgrade1 += 28 * _ingameManager.amount_Upgrade1;
                    _text_LevelTexts[0].text = "Lv. " + _ingameManager.amount_Upgrade1;
                    _text_CostTexts[0].text = _ingameManager.cost_Upgrade1.ToString();
                }
                break;
            case "Button_Upgrade2":
                if (_ingameManager.CheckGold(_ingameManager.cost_Upgrade2))
                {
                    _ingameManager.GetGold(_ingameManager.cost_Upgrade2);
                    _ingameManager.amount_Upgrade2++;
                    _ingameManager.cost_Upgrade2 += 30 * _ingameManager.amount_Upgrade2;
                    _text_LevelTexts[1].text = "Lv. " + _ingameManager.amount_Upgrade2;
                    _text_CostTexts[1].text = _ingameManager.cost_Upgrade2.ToString();
                }
                break;
            case "Button_Upgrade3":
                if (_ingameManager.CheckGold(_ingameManager.cost_Upgrade3))
                {
                    _ingameManager.GetGold(_ingameManager.cost_Upgrade3);
                    _ingameManager.amount_Upgrade3++;
                    _ingameManager.cost_Upgrade3 += 32 * _ingameManager.amount_Upgrade3;
                    _text_LevelTexts[2].text = "Lv. " + _ingameManager.amount_Upgrade3;
                    _text_CostTexts[2].text = _ingameManager.cost_Upgrade3.ToString();
                }
                break;
            case "Button_AnimationHide":
                _animator.SetTrigger("Off");
                break;
        }
    }
}