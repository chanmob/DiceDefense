/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-02-25 오후 7:29:50
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class GooglePlayManager : Singleton<GooglePlayManager>
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/


    /*----------------[PUBLIC METHOD]------------------------------*/

    public bool GooglePlayLogine()
    {
        bool result = false;

        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    result = true;
                    Debug.Log("로그인 성공");
                }
                else
                {
                    result = false;
                    Debug.Log("로그인 실패");
                }
            });
        }
        else
        {
            result = true;
            Debug.Log("이미 로그인");
        }

        return result;
    }

    //public void ShowRanking()
    //{
    //    PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard);
    //}

    //public void UploadRanking(int _score)
    //{
    //    Social.ReportScore(_score, GPGSIds.leaderboard, (bool succsee) =>
    //    {
    //        if (succsee)
    //        {
    //            ObscuredPrefs.SetInt("UPLOADSCORE", _score);
    //            Debug.Log("업로드 성공");
    //        }
    //        else
    //        {
    //            ObscuredPrefs.DeleteKey("UPLOADSCORE");
    //        }
    //    }
    //    );
    //}

    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void GetAchievement(string _id)
    {
        Social.ReportProgress(_id, 100f, null);
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
}