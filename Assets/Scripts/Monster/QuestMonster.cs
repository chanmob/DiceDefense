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

    public void SetQuestMonster()
    {
        if (questIndex == -1)
        {
            Debug.Log("퀘스트 몬스터 인덱스 에러");
            return;
        }

        SetHP((int)Mathf.Pow(questIndex, 2) * 400);
        speed += 0.15f * (questIndex % 2);
        _shield = questIndex >= 3 ? 50 * (int)Mathf.Pow(questIndex, 2) : 0;
        if (_shield > 0)
            _shieldTransform.gameObject.SetActive(true);
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Die()
    {
        InGameManager.instance.monsterList.Remove(this);
        Destroy(this.gameObject);
    }

    protected override void Enable()
    {

    }

    protected override void Disable()
    {
        _ingameManager.GetGold(questIndex * 200);
        //_ingameManager.GetGold((int)Mathf.Pow(questIndex, 2) * 100);
    }
}