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
    }

    protected override void Enable()
    {
        _shield = 100;
    }
}