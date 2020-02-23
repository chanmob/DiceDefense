/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-02-04 오후 4:29:26
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_HowToPlay : UI_Base
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private GameObject _tutorialObject;

    private int _totalTutorial = 0;
    private int _currentTutorial = 0;

    [SerializeField] private Text _text_Count;

    /*----------------[PUBLIC METHOD]------------------------------*/

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Start()
    {
        base.Start();

        _totalTutorial = _tutorialObject.transform.childCount;

        _text_Count.text = (_currentTutorial + 1) + " / " + _totalTutorial;
    }

    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "Button_Next":
                MoveTutorial(true);
                break;
            case "Button_Pre":
                MoveTutorial(false);
                break;
        }
    }

    private void MoveTutorial(bool next)
    {
        if (next)
            _currentTutorial++;
        else
            _currentTutorial--;

        _currentTutorial = Mathf.Clamp(_currentTutorial, 0, _totalTutorial - 1);

        for(int i = 0; i < _totalTutorial; i++)
        {
            _tutorialObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        _tutorialObject.transform.GetChild(_currentTutorial).gameObject.SetActive(true);

        _text_Count.text = (_currentTutorial + 1) + " / " + _totalTutorial;
    }
}