/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-26 오후 6:45:19
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_UnitAttribute : UI_Base
{
    /* [PUBLIC VARIABLE]					*/

    [HideInInspector] public int unitIndex = -1;
    [HideInInspector] public bool isSecondAttribute = false;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private GameObject _firstAttribute;
    [SerializeField] private GameObject _secondAttribute;

    private UnitManager _unitManager;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void ShowForSetAttribute(int index)
    {
        unitIndex = index;
        Show();
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private void OnEnable()
    {
        Time.timeScale = 0;

        if (isSecondAttribute)
        {
            int len = _secondAttribute.transform.childCount;
            for (int i = 0; i < len; i++)
            {
                _secondAttribute.transform.GetChild(i).gameObject.SetActive(true);
            }

            int randomIdx = Random.Range(0, len);
            _secondAttribute.transform.GetChild(randomIdx).gameObject.SetActive(false);
        }
        else
        {
            int len = _firstAttribute.transform.childCount;
            for(int i = 0; i < len; i++)
            {
                _firstAttribute.transform.GetChild(i).gameObject.SetActive(true);
            }

            int randomIdx = Random.Range(0, len);
            _firstAttribute.transform.GetChild(randomIdx).gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        unitIndex = -1;
        Time.timeScale = 1;
    }

    protected override void Start()
    {
        base.Start();

        if (_unitManager == null)
            _unitManager = UnitManager.instance;
    }

    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "Button_FirstAttribute1":
                SetUnitAttribute(DataDefine.Attribute.Air);
                break;
            case "Button_FirstAttribute2":
                SetUnitAttribute(DataDefine.Attribute.Fire);
                break;
            case "Button_FirstAttribute3":
                SetUnitAttribute(DataDefine.Attribute.Ice);
                break;
            case "Button_FirstAttribute4":
                SetUnitAttribute(DataDefine.Attribute.Lava);
                break;

            case "Button_SecondAttribute1":
                SetUnitAttribute(DataDefine.Attribute.Mystery);
                break;
            case "Button_SecondAttribute2":
                SetUnitAttribute(DataDefine.Attribute.Nature);
                break;
            case "Button_SecondAttribute3":
                SetUnitAttribute(DataDefine.Attribute.Storm);
                break;
            case "Button_SecondAttribute4":
                SetUnitAttribute(DataDefine.Attribute.Water);
                break;
        }
    }

    private void SetUnitAttribute(DataDefine.Attribute attribute)
    {
        switch (unitIndex)
        {
            case -1:
                Debug.LogError("Unit Index Error");
                break;
            case 0:
                if (isSecondAttribute)
                {
                    _unitManager.SetUnitSecondAttribute(DataDefine.UnitType.Unit1, attribute);
                }
                else
                {
                    _unitManager.SetUnitFirstAttribute(DataDefine.UnitType.Unit1, attribute);
                }
                break;
            case 1:
                if (isSecondAttribute)
                {
                    _unitManager.SetUnitSecondAttribute(DataDefine.UnitType.Unit2, attribute);
                }
                else
                {
                    _unitManager.SetUnitFirstAttribute(DataDefine.UnitType.Unit2, attribute);
                }
                break;
            case 2:
                if (isSecondAttribute)
                {
                    _unitManager.SetUnitSecondAttribute(DataDefine.UnitType.Unit3, attribute);
                }
                else
                {
                    _unitManager.SetUnitFirstAttribute(DataDefine.UnitType.Unit3, attribute);
                }
                break;
        }

        Hide();
    }
}