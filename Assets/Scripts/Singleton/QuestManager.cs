/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-31 오전 11:26:30
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    /* [PUBLIC VARIABLE]					*/

    public int[,] questUnitCount;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private bool[] questList;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void CheckQuest()
    {
        if(questList[0] == false)
        {
            List<int> temp = new List<int>();

            for(int i = 0; i < 3; i++)
            {
                temp.Add(questUnitCount[i, 0]);
            }

            if(QuestMultiValue(temp) != 0)
            {
                questList[1] = true;
            }
        }

        if(questList[1] == false)
        {

        }
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Awake()
    {
        base.Awake();

        questUnitCount = new int[9, 6];

        questList = new bool[10];
    }

    private int QuestMultiValue(List<int> list)
    {
        int result = 1;
        int len = list.Count;

        for (int i = 0; i < len; i++)
        {
            result *= list[i];
        }

        return result;
    }

    private int QuestAddValue(List<int> list)
    {
        int result = 0;
        int len = list.Count;

        for (int i = 0; i < len; i++)
        {
            result += list[i];
        }

        return result;
    }
}