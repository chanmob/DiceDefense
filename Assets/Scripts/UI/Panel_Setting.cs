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

    [SerializeField] private Image[] _image_Sound = new Image[2];

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
            case "Button_Sound":
                SoundManager.instance.VolumeOnOff();

                if (AudioListener.volume == 1)
                {
                    _image_Sound[1].gameObject.SetActive(false);
                    _image_Sound[0].gameObject.SetActive(true);
                }
                else
                {
                    _image_Sound[0].gameObject.SetActive(false);
                    _image_Sound[1].gameObject.SetActive(true);
                }
                break;
            case "Button_Language":
                InGameUIManager.instance.popup_Language.Open();
                break;
            case "Button_Retry":
                SceneControl.instance.SceneLoad("InGame");
                break;
            case "Button_Main":
                SceneControl.instance.SceneLoad("OutGame");
                break;
            case "Button_AnimationHide":
                _animator.SetTrigger("Off");
                break;
        }
    }
}