/*
  ============================================
	Author	: user
	Time 	: 2020-01-14 오후 10:47:29
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    /* [PUBLIC VARIABLE]					*/

    public List<Monster> monsterList;

    /* [PROTECTED && PRIVATE VARIABLE]		*/


    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Awake()
    {
        base.Awake();

        monsterList = new List<Monster>();
    }

    private void MakeMonsterPool(int count)
    {
        for(int i = 0; i < count; i++)
        {

        }
    }
}