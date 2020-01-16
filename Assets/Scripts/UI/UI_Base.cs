using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Base : UI_ElementBase
{
    private const string Button_HideName = "Button_Hide";

    [SerializeField] private bool _defaultShow;

    public void Show()
    {
        gameObjectCached.SetActive(true);
    }

    public void Hide()
    {
        gameObjectCached.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        if (_defaultShow == false)
            SetActive(false);
    }

    protected override void OnClickButtons(string buttonName)
    {
        if (buttonName.Equals(Button_HideName))
            Hide();
    }
}