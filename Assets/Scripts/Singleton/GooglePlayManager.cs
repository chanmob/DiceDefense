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


    public void ShowRanking(string id)
    {
        if (GooglePlayLogine() == false)
            return;

        PlayGamesPlatform.Instance.ShowLeaderboardUI(id);
    }

    public void UploadRanking(string id, int _score, Action successAction = null, Action failAction = null)
    {
        if (GooglePlayLogine() == false)
            return;

        Social.ReportScore(_score, id, (bool succsee) =>
        {
            if (succsee)
            {
                successAction?.Invoke();
            }
            else
            {
                failAction?.Invoke();
            }
        }
        );
    }

    public void ShowAchievements()
    {
        if (GooglePlayLogine() == false)
            return;

        Social.ShowAchievementsUI();
    }

    public void GetAchievement(string _id)
    {
        if (GooglePlayLogine() == false)
            return;

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

    private bool GooglePlayLogine()
    {
        bool result = false;

        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            });
        }
        else
        {
            result = true;
        }

        return result;
    }
}