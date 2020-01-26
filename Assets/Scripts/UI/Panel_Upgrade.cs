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
                _ingameManager.amount_Upgrade1++;
                break;
            case "Button_Upgrade2":
                _ingameManager.amount_Upgrade2++;
                break;
            case "Button_Upgrade3":
                _ingameManager.amount_Upgrade3++;
                break;
            case "Button_AnimationHide":
                _animator.SetTrigger("Off");
                break;
        }
    }
}