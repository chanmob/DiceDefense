/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-26 오후 6:19:28
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMonster : Monster
{
    /* [PUBLIC VARIABLE]					*/

    public int questIndex = -1;

    /* [PROTECTED && PRIVATE VARIABLE]		*/


    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Die()
    {
    }

    protected override void Enable()
    {
        if(questIndex == -1)
        {
            Debug.Log("퀘스트 몬스터 인덱스 에러");
            return;
        }

        _hp = (int)Mathf.Pow(questIndex, 2) * 150;
    }

    protected override void Disable()
    {
        _ingameManager.GetGold((int)Mathf.Pow(questIndex, 2) * 200);
    }
}