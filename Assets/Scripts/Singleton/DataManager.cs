/*
  ============================================
	Author	: 김찬영
	Time 	: 2020-02-02 오후 9:28:20
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class DataManager : Singleton<DataManager>
{
    /* [PUBLIC VARIABLE]					*/

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private const string roundSaveName = "BestRound";

    /*----------------[PUBLIC METHOD]------------------------------*/

    public bool CheckRenewal(int round)
    {
        if (ObscuredPrefs.HasKey(roundSaveName) == false)
            return false;

        return ObscuredPrefs.GetInt(roundSaveName) < round;
    }

    public void SaveBestRound(int round)
    {
        ObscuredPrefs.SetInt(roundSaveName, round);
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/
}