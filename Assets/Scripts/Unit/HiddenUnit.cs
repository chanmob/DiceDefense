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
        power = Random.Range(10, 30);

        int randomAttackSpeed = Random.Range(0, 2);
        switch (randomAttackSpeed)
        {
            case 0:
                attackSpeed = 0.2f;
                break;
            case 1:
                attackSpeed = 0.3f;
                break;
            case 2:
                attackSpeed = 0.4f;
                break;
        }
    }
}