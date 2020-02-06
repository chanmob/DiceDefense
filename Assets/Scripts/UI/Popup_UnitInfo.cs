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

public class Popup_UnitInfo : UI_Base
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
        switch (unit.unitRanking)
        {
            case DataDefine.UnitRanking.Normal:
                _text_UnitName.text = "일반 유닛";
                SetUnitSprite(unit.unitType, false);
                break;
            case DataDefine.UnitRanking.Super:
                _text_UnitName.text = "슈퍼 유닛";
                SetUnitSprite(unit.unitType, false);
                break;
            case DataDefine.UnitRanking.Hidden:
                _text_UnitName.text = "히든 유닛";
                SetUnitSprite(unit.unitType, true);
                break;
        }
        
        _text_Level.text = unit.unitLevel.ToString();
        _text_Power.text = unit.power.ToString();
        _text_Speed.text = unit.attackSpeed.ToString();

        Show();
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private void SetUnitSprite(DataDefine.UnitType type, bool hidden)
    {
        if (hidden)
        {
            switch (type)
            {
                case DataDefine.UnitType.Unit1:
                    _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_HiddenUndead");
                    break;
                case DataDefine.UnitType.Unit2:
                    _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_HiddenKnight");
                    break;
                case DataDefine.UnitType.Unit3:
                    _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_HiddenRouge");
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case DataDefine.UnitType.Unit1:
                    _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_NormalUndead");
                    break;
                case DataDefine.UnitType.Unit2:
                    _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_NormalKnight");
                    break;
                case DataDefine.UnitType.Unit3:
                    _image_UnitImage.sprite = ResourceManager.instance.GetObject<Sprite>("Head_NormalRouge");
                    break;
            }
        }
    }
}