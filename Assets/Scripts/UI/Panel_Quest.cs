/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-15 오후 11:15:47
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Quest : UI_Base
{
    /* [PUBLIC VARIABLE]					*/

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private float[] _questCoolTimes;

    [SerializeField] private Image[] _questCoolTimeImages;

    [SerializeField] private Button[] _questButtons;

    private Animator _animator;

    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
    }


    protected override void OnClickButtons(string buttonName)
    {
        base.OnClickButtons(buttonName);

        switch (buttonName)
        {
            case "Button_AnimationHide":
                _animator.SetTrigger("Off");
                break;
            case "Button_Quest1":
                CreateQuestMonster(1);
                break;
            case "Button_Quest2":
                CreateQuestMonster(2);
                break;
            case "Button_Quest3":
                CreateQuestMonster(3);
                break;
            case "Button_Quest4":
                CreateQuestMonster(4);
                break;
            case "Button_Quest5":
                CreateQuestMonster(5);
                break;
            case "Button_Quest6":
                CreateQuestMonster(6);
                break;
        }
    }

    private void CreateQuestMonster(int level)
    {
        QuestMonster questMonsterPrefab = ResourceManager.instance.GetMonoBehavioursObject<QuestMonster>("QuestMonster");
        QuestMonster questMonster = Instantiate(questMonsterPrefab);
        questMonster.questIndex = level;
        questMonster.SetQuestMonster();
        questMonster.transform.SetParent(null);
        InGameManager.instance.monsterList.Add(questMonster);
    }

    private IEnumerator WaitWaveCoroutine(int idx)
    {
        Image timeImage = _questCoolTimeImages[idx];
        _questButtons[idx].interactable = false;

        timeImage.fillAmount = 1;

        float cooltime = _questCoolTimes[idx];
        float checkTime = cooltime;

        while (timeImage.fillAmount > 0)
        {
            timeImage.fillAmount -= Time.deltaTime / cooltime;
            checkTime -= Time.deltaTime;

            yield return null;
        }

        _questButtons[idx].interactable = true;
    }
}