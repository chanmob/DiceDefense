/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-02-02 오후 9:52:37
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_UnitInfo : UI_Base
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private Image _image_UnitImage;

    [SerializeField] private Text _text_UnitName;
    [SerializeField] private Text _text_Level;
    [SerializeField] private Text _text_Power;
    [SerializeField] private Text _text_Speed;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void ShowUnitInfo(Unit unit)
    {
        switch (unit.unitName)
        {
            case "Normal_Undead":
                _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_NormalUndead");
                break;
            case "Super_Undead":
                _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_NormalUndead");
                break;
            case "Hidden_Undead":
                _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_HiddenUndead");
                break;

            case "Normal_Knight":
                _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_NormalKnight");
                break;
            case "Super_Knight":
                _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_NormalKnight");
                break;
            case "Hidden_Knight":
                _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_HiddenKnight");
                break;

            case "Normal_Rouge":
                _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_NormalRouge");
                break;
            case "Super_Rouge":
                _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_NormalRouge");
                break;
            case "Hidden_Rouge":
                _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_HiddenRouge");
                break;
        }
        
        _text_Level.text = unit.unitLevel.ToString();
        _text_Power.text = unit.power.ToString();
        _text_Speed.text = unit.attackSpeed.ToString();

        Show();
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/


}