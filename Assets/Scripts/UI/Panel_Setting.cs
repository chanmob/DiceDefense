/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-15 오후 11:12:47
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Setting : UI_Base
{
    /* [PUBLIC VARIABLE]					*/

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private Animator _animator;

    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
    }

    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "Button_AnimationHide":
                _animator.SetTrigger("Off");
                break;
        }
    }
}