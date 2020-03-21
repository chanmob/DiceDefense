/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-31 오후 12:09:06
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Panel_Result : UI_Base
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private GameObject _noReward;

    [SerializeField] private Text _text_Round;
    [SerializeField] private Text _text_Best;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void ShowResult(int round, bool renewal = false)
    {
        Show();
        transform.DOScale(1, 1).SetEase(Ease.OutBack).OnComplete(() => 
        {
            RoundTextCounting(round, renewal);
            Time.timeScale = 0;
        });
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private void RoundTextCounting(int round, bool renewal = false)
    {
        StartCount(_text_Round, 0, round, 1, renewal);
    }

    private void StartCount(Text text, float min, float max, float time, bool renewal)
    {
        if (renewal)
            StartCoroutine(CountingBestRoundCoroutine(_text_Best, 0, max, time));
        else
            StartCoroutine(CountingCurrentRoundCoroutine(text, min, max, time));
    }

    private IEnumerator CountingCurrentRoundCoroutine(Text text, float min, float max, float time)
    {
        float offset = (max - min) / time;

        while (min < max)
        {
            min += offset * Time.unscaledDeltaTime;
            text.text = LanguageManager.instance.GetCurrentLanguageText("Text_CurrenWave") + " : " + ((int)min).ToString();
            yield return null;
        }

        min = max;
        text.text = LanguageManager.instance.GetCurrentLanguageText("Text_CurrenWave") + " : " + ((int)min).ToString();
    }

    private IEnumerator CountingBestRoundCoroutine(Text text, float min, float max, float time)
    {
        yield return StartCoroutine(CountingCurrentRoundCoroutine(_text_Round, 0, max, time));

        float offset = (max - min) / time;

        while (min < max)
        {
            min += offset * Time.unscaledDeltaTime;
            text.text = LanguageManager.instance.GetCurrentLanguageText("Text_BestWave") + " : " + ((int)min).ToString();
            yield return null;
        }

        min = max;
        text.text = LanguageManager.instance.GetCurrentLanguageText("Text_BestWave") + " : " + ((int)min).ToString();
    }

    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "Button_Continue":
                AdMobManager.instance.rewardFailEvent += () => 
                {
                    _noReward.SetActive(true);
                };
                AdMobManager.instance.afterRewardEvent += () =>
                {
                    InGameManager.instance.Restart();
                };

                AdMobManager.instance.ShowRewardAD();
                break;
            case "Button_RePlay":
                SceneControl.instance.SceneLoad("InGame");
                break;
            case "Button_Exit":
                SceneControl.instance.SceneLoad("OutGame");
                break;
            case "Button_RewardClose":
                _noReward.SetActive(false);
                AdMobManager.instance.EventRelease();
                break;
        }
    }
}