/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-02-04 오후 4:22:19
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_MainOutGame : UI_Base
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private Image[] _image_Sound = new Image[2];

    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Start()
    {
        base.Start();

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
    }

    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "Button_GamePlay":
                SceneControl.instance.SceneLoad("InGame");
                break;
            case "Button_HowToPlay":
                OutGameUIManager.instance.panel_HowToPlay.Show();
                break;
            case "Button_Exit":
                OutGameUIManager.instance.popup_GameExit.Open();
                break;
            case "Button_Store":
                Application.OpenURL("");
                break;
            case "Button_Sound":
                SoundManager.instance.VolumeOnOff();

                if(AudioListener.volume == 1)
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
            case "Button_Ranking":
                break;
            case "Button_Archieve":
                break;
        }
    }
}