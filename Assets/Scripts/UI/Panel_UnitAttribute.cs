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

        int len = _firstAttribute.transform.childCount;
        for (int i = 0; i < len; i++)
        {
            _firstAttribute.transform.GetChild(i).gameObject.SetActive(true);
        }

        int randomIdx = Random.Range(0, len);
        _firstAttribute.transform.GetChild(randomIdx).gameObject.SetActive(false);
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
                SetUnitAttribute(DataDefine.Attribute.Cloud);
                break;
            case "Button_FirstAttribute2":
                SetUnitAttribute(DataDefine.Attribute.Fire);
                break;
            case "Button_FirstAttribute3":
                SetUnitAttribute(DataDefine.Attribute.Water);
                break;
            case "Button_FirstAttribute4":
                SetUnitAttribute(DataDefine.Attribute.Mountain);
                break;

            case "Button_SecondAttribute1":
                SetUnitAttribute(DataDefine.Attribute.Second1);
                break;
            case "Button_SecondAttribute2":
                SetUnitAttribute(DataDefine.Attribute.Second2);
                break;
            case "Button_SecondAttribute3":
                SetUnitAttribute(DataDefine.Attribute.Second3);
                break;
            case "Button_SecondAttribute4":
                SetUnitAttribute(DataDefine.Attribute.Second4);
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
                _unitManager.SetUnitFirstAttribute(DataDefine.UnitType.Unit1, attribute);
                break;
            case 1:
                _unitManager.SetUnitFirstAttribute(DataDefine.UnitType.Unit2, attribute);
                break;
            case 2:
                _unitManager.SetUnitFirstAttribute(DataDefine.UnitType.Unit3, attribute);
                break;
        }

        Hide();
    }
}