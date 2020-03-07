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

    [SerializeField] public GameObject[] _cooltimeObjects;

    [SerializeField] public Image[] _questCoolTimeImages;

    [SerializeField] public Button[] _questButtons;

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
        QuestCoolTimeManager.instance.QuestStarted(level);
        QuestMonster questMonsterPrefab = ResourceManager.instance.GetMonoBehavioursObject<QuestMonster>("QuestMonster");
        QuestMonster questMonster = Instantiate(questMonsterPrefab);
        questMonster.questIndex = level;
        questMonster.SetQuestMonster();
        questMonster.transform.SetParent(null);
        InGameManager.instance.monsterList.Add(questMonster);
    }
}