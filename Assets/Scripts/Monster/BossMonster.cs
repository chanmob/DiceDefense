/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-26 오후 6:19:20
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/


    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Die()
    {
        _ingameManager.monsterList.Remove(this);
        _ingameManager.roundCheckMonster.Remove(this);
        _ingameManager.MonsterUIRefresh();
        gameObject.SetActive(false);
    }

    protected override void Disable()
    {
        _ingameManager.GetGold(_ingameManager.round * 10);
    }

    protected override void Enable()
    {
        SetHP((int)Mathf.Pow((_ingameManager.round / 10), 2) * 1000);
        _shield = _ingameManager.round * 10;
    }
}