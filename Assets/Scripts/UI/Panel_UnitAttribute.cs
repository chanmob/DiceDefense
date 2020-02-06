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

    private InGameManager _ingameManager;

    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private void OnEnable()
    {
        if (isSecondAttribute)
        {
            int len = _secondAttribute.transform.childCount;
            int randomIdx = Random.Range(0, len);

            _secondAttribute.transform.GetChild(randomIdx).gameObject.SetActive(false);
        }
        else
        {
            int len = _firstAttribute.transform.childCount;
            int randomIdx = Random.Range(0, len);

            _firstAttribute.transform.GetChild(randomIdx).gameObject.SetActive(false);
        }
    }

    protected override void Start()
    {
        base.Start();

        if (_ingameManager == null)
            _ingameManager = InGameManager.instance;
    }

    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "SetAttribute":
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
                    _ingameManager.unitOneSecondAttribute = attribute;
                }
                else
                {
                    _ingameManager.unitOneFirstAttribute = attribute;
                }
                break;
            case 1:
                if (isSecondAttribute)
                {
                    _ingameManager.unitTwoSecondAttribute = attribute;
                }
                else
                {
                    _ingameManager.unitTwoFirstAttribute = attribute;
                }
                break;
            case 2:
                if (isSecondAttribute)
                {
                    _ingameManager.unitThreeSecondAttribute = attribute;
                }
                else
                {
                    _ingameManager.unitThreeFirstAttribute = attribute;
                }
                break;
        }
    }
}