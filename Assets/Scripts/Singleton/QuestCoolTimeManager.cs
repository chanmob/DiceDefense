/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-03-08 오전 12:52:29
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCoolTimeManager : Singleton<QuestCoolTimeManager>
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private float[] _questCoolTimes;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void QuestStarted(int number)
    {
        StartCoroutine(QuestCoolTimeCoroutine(number));
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private IEnumerator QuestCoolTimeCoroutine(int number)
    {
        int idx = number - 1;

        Panel_Quest quest = InGameUIManager.instance.panel_Quest;

        quest._cooltimeObjects[idx].SetActive(true);
        UnityEngine.UI.Image timeImage = quest._questCoolTimeImages[idx];
        quest._questButtons[idx].interactable = false;

        timeImage.fillAmount = 0;

        float cooltime = _questCoolTimes[idx];

        while (timeImage.fillAmount < 1)
        {
            timeImage.fillAmount += Time.deltaTime / cooltime;
            yield return null;
        }

        quest._cooltimeObjects[idx].SetActive(false);
        quest._questButtons[idx].interactable = true;
    }
}