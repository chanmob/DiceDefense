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

    }

    protected override void Disable()
    {
        _ingameManager.GetGold(_ingameManager.round * 100);
    }

    protected override void Enable()
    {
        _shield = 100;
        _hp = (int)Mathf.Pow((_ingameManager.round / 10), 2) * 100;
    }
}