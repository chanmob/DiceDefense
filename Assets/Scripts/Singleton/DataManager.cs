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
        Sound,
        UploadScrore
    }

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private const string roundSaveName = "BestRound";
    private const string soundSaveName = "SoundOnOff";
    private const string uploadScoreSaveName = "UploadRound";

    /*----------------[PUBLIC METHOD]------------------------------*/

    public bool CheckRenewal(int round)
    {
        if (ObscuredPrefs.HasKey(roundSaveName) == false)
            return true;

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
            case SaveDataType.UploadScrore:
                ObscuredPrefs.SetInt(uploadScoreSaveName, value);
                break;
        }
    }

    public int GetIntData(SaveDataType type)
    {
        if (CheckData(type) == false)
            return 0;

        switch (type)
        {
            case SaveDataType.Round:
                return ObscuredPrefs.GetInt(roundSaveName);
            case SaveDataType.Sound:
                return ObscuredPrefs.GetInt(soundSaveName);
            case SaveDataType.UploadScrore:
                return ObscuredPrefs.GetInt(uploadScoreSaveName);
            default:
                return 0;
        }
    }

    public bool CheckData(SaveDataType type)
    {
        switch (type)
        {
            case SaveDataType.Round:
                return ObscuredPrefs.HasKey(roundSaveName);
            case SaveDataType.Sound:
                return ObscuredPrefs.HasKey(soundSaveName);
            case SaveDataType.UploadScrore:
                return ObscuredPrefs.HasKey(uploadScoreSaveName);
            default:
                return false;
        }
    }

    public void DeleteKey(SaveDataType type)
    {
        switch (type)
        {
            case SaveDataType.Round:
                ObscuredPrefs.DeleteKey(roundSaveName);
                break;
            case SaveDataType.Sound:
                ObscuredPrefs.DeleteKey(soundSaveName);
                break;
            case SaveDataType.UploadScrore:
                ObscuredPrefs.DeleteKey(uploadScoreSaveName);
                break;
        }
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/


}