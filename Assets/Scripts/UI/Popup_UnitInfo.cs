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
using DG.Tweening;

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
                _text_UnitName.text = LanguageManager.instance.GetCurrentLanguageText("Text_UnitName_Normal");
                break;
            case DataDefine.UnitRanking.Super:
                _text_UnitName.text = LanguageManager.instance.GetCurrentLanguageText("Text_UnitName_Super");
                break;
            case DataDefine.UnitRanking.Hidden:
                _text_UnitName.text = LanguageManager.instance.GetCurrentLanguageText("Text_UnitName_Hidden");
                break;
        }

        SetUnitSprite(unit.unitType);
        _text_Level.text = LanguageManager.instance.GetCurrentLanguageText("Text_Level") + " : " + (unit.unitLevel + 1).ToString();

        switch (unit.unitType)
        {
            case DataDefine.UnitType.Unit1:
                _text_Power.text = LanguageManager.instance.GetCurrentLanguageText("Text_Power") + 
                    " : " + (unit.power + InGameManager.instance.amount_Upgrade1).ToString() + 
                    "(" + unit.power + "+" + InGameManager.instance.amount_Upgrade1 + ")";
                break;
            case DataDefine.UnitType.Unit2:
                _text_Power.text = LanguageManager.instance.GetCurrentLanguageText("Text_Power") + 
                    " : " + (unit.power + InGameManager.instance.amount_Upgrade2).ToString() + 
                    "(" + unit.power + "+" + InGameManager.instance.amount_Upgrade2 + ")";
                break;
            case DataDefine.UnitType.Unit3:
                _text_Power.text = LanguageManager.instance.GetCurrentLanguageText("Text_Power") + 
                    " : " + (unit.power + InGameManager.instance.amount_Upgrade3).ToString() + 
                    "(" + unit.power + "+" + InGameManager.instance.amount_Upgrade3 + ")";
                break;
        }

        _text_Speed.text = LanguageManager.instance.GetCurrentLanguageText("Text_Speed") + " : " + unit.attackSpeed.ToString();

        Show();
        transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private void SetUnitSprite(DataDefine.UnitType type)
    {
        switch (type)
        {
            case DataDefine.UnitType.Unit1:
                _image_UnitImage.color = new Color32(198, 12, 49, 255);
                break;
            case DataDefine.UnitType.Unit2:
                _image_UnitImage.color = new Color32(0, 52, 120, 255);
                break;
            case DataDefine.UnitType.Unit3:
                _image_UnitImage.color = new Color32(54, 60, 64, 255);
                break;
        }
    }

    protected override void OnClickButtons(string buttonName)
    {
        switch (buttonName)
        {
            case "Button_Hide":
                transform.DOScale(0, 0.25f).SetEase(Ease.InQuad).OnComplete(() => Hide());
                break;
        }
    }
}