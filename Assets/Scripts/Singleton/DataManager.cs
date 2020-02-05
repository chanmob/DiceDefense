/*
  ============================================
	Author	: ChanMob
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

    public enum SaveDataType
    {
        Round,
        Sound
    }

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private const string roundSaveName = "BestRound";
    private const string soundSaveName = "SoundOnOff";

    /*----------------[PUBLIC METHOD]------------------------------*/

    public bool CheckRenewal(int round)
    {
        if (ObscuredPrefs.HasKey(roundSaveName) == false)
            return false;

        return ObscuredPrefs.GetInt(roundSaveName) < round;
    }

    public void SaveIntData(SaveDataType type, int value)
    {
        switch (type)
        {
            case SaveDataType.Round:
                ObscuredPrefs.SetInt(roundSaveName, value);
                break;
            case SaveDataType.Sound:
                ObscuredPrefs.SetInt(soundSaveName, value);
                break;
        }
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/
}