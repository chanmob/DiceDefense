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
    [SerializeField] private Text _text_Attribute;

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
        
        _text_Level.text = "레벨 : " + (unit.unitLevel + 1).ToString();

        switch (unit.unitType)
        {
            case DataDefine.UnitType.Unit1:
                _text_Power.text = "공격력 : " + (unit.power + InGameManager.instance.amount_Upgrade1).ToString() + "(" + unit.power + "+" + InGameManager.instance.amount_Upgrade1 + ")";
                break;
            case DataDefine.UnitType.Unit2:
                _text_Power.text = "공격력 : " + (unit.power + InGameManager.instance.amount_Upgrade2).ToString() + "(" + unit.power + "+" + InGameManager.instance.amount_Upgrade2 + ")";
                break;
            case DataDefine.UnitType.Unit3:
                _text_Power.text = "공격력 : " + (unit.power + InGameManager.instance.amount_Upgrade3).ToString() + "(" + unit.power + "+" + InGameManager.instance.amount_Upgrade3 + ")";
                break;
        }

        _text_Speed.text = "공격속도 : " + unit.attackSpeed.ToString();

        SetAttributeText(unit.firstAttribue);

        Show();
        transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
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

    protected override void OnClickButtons(string buttonName)
    {
        switch (buttonName)
        {
            case "Button_Hide":
                transform.DOScale(0, 0.25f).SetEase(Ease.InQuad).OnComplete(() => Hide());
                break;
        }
    }

    private void SetAttributeText(DataDefine.Attribute first)
    {
        string firstAttribute = "없음";
        
        switch (first)
        {
            case DataDefine.Attribute.Cloud:
                firstAttribute = "바람";
                break;

            case DataDefine.Attribute.Fire:
                firstAttribute = "불";
                break;

            case DataDefine.Attribute.Water:
                firstAttribute = "물";
                break;

            case DataDefine.Attribute.Mountain:
                firstAttribute = "대지";
                break;
        }

        _text_Attribute.text = string.Format("속성 : {0}", firstAttribute);
    }
}