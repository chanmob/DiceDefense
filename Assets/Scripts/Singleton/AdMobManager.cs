/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-02-25 오후 7:19:58
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobManager : Singleton<AdMobManager>
{
    /* [PUBLIC VARIABLE]					*/

    public Action afterRewardEvent;
    public Action rewardFailEvent;

    public bool isTest;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private readonly string testBannerID = "ca-app-pub-3940256099942544/6300978111";
    private readonly string testScreenID = "ca-app-pub-3940256099942544/1033173712";
    private readonly string testRewardID = "ca-app-pub-3940256099942544/5224354917";

    private readonly string bannerID = "ca-app-pub-9954381112163314/6416083314";
    private readonly string screenID = "ca-app-pub-9954381112163314/7537593294";
    private readonly string rewardID = "ca-app-pub-9954381112163314/9434990717";

    private BannerView bannerAD;
    private InterstitialAd screenAD;
    private RewardBasedVideoAd rewardedAD;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void ShowBannerAD()
    {
        if (bannerAD == null)
            InitBannerAD();

        bannerAD.Show();
    }

    public void HideBannerAD()
    {
        bannerAD.Hide();
    }

    public void ShowScreenAD()
    {
        InitScreenAD();
        StartCoroutine(ShowScreenADCoroutine());
    }

    public void ShowRewardAD()
    {
        RewardCoroutine();
    }

    public void EventRelease()
    {
        if (afterRewardEvent != null)
            afterRewardEvent = null;

        if (rewardFailEvent != null)
            rewardFailEvent = null;
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    void Start()
    {
        InitBannerAD();

        if (rewardedAD == null)
            InitRewardedAD();
    }

    private IEnumerator ShowScreenADCoroutine()
    {
        yield return new WaitForSeconds(1f);

        while (!screenAD.IsLoaded())
        {
            yield return null;
        }

        screenAD.Show();
    }

    private void InitBannerAD()
    {
        string id = isTest ? testBannerID : bannerID;

        bannerAD = new BannerView(id, AdSize.SmartBanner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerAD.LoadAd(request);
    }

    private void InitScreenAD()
    {
        string id = isTest ? testScreenID : screenID;

        screenAD = new InterstitialAd(id);

        AdRequest request = new AdRequest.Builder().Build();

        screenAD.LoadAd(request);
    }


    private void RewardCoroutine()
    {
        if (!rewardedAD.IsLoaded())
        {
            InitRewardedAD();

            rewardFailEvent?.Invoke();
            Debug.Log("보상형 광고 로드 안됨");
            return;
        }
        else
        {
            Debug.Log("보상형 광고 로드 완료");
        }

        rewardedAD.Show();
    }

    private void InitRewardedAD()
    {
        string id = isTest ? testRewardID : rewardID;

        rewardedAD = RewardBasedVideoAd.Instance;

        AdRequest request = new AdRequest.Builder().Build();

        rewardedAD.LoadAd(request, id);
        rewardedAD.OnAdLoaded += RewardedADLoad;
        rewardedAD.OnAdClosed += RewardedADClose;
        rewardedAD.OnAdRewarded += RewardToUesr;

        Debug.Log("리워드 광고 생성");
    }

    private void RewardedADLoad(object sender, EventArgs arg)
    {
        Debug.Log("보상형 광고 로드");
    }

    private void RewardedADClose(object sender, EventArgs arg)
    {
        Debug.Log("보상형 광고 닫힘");
        InitRewardedAD();
    }

    private void RewardToUesr(object sender, EventArgs arg)
    {
        afterRewardEvent?.Invoke();
        rewardedAD.OnAdLoaded -= RewardedADLoad;
        rewardedAD.OnAdClosed -= RewardedADClose;
        rewardedAD.OnAdRewarded -= RewardToUesr;
    }
}