/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-30 오후 3:25:12
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenUnit : Unit
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/


    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void OnStart()
    {
        power = Random.Range(1, 35);

        int randomAttackSpeed = Random.Range(1, 7);
        attackSpeed = randomAttackSpeed * 0.1f;
    }
}