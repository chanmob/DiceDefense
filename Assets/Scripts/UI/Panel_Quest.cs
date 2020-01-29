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
        }
    }

    private void CreateQuestMonster(int level)
    {
        QuestMonster questMonsterPrefab = ResourceManager.instance.GetMonoBehavioursObject<QuestMonster>("QuestMonster");
        QuestMonster questMonster = Instantiate(questMonsterPrefab);
        questMonster.questIndex = level;
        questMonster.transform.SetParent(null);
        InGameManager.instance.monsterList.Add(questMonster);
    }
}