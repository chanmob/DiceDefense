/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-14 오후 11:37:32
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InGameManager : Singleton<InGameManager>
{
    /* [PUBLIC VARIABLE]					*/

	public List<Monster> roundCheckMonster;
	public List<Monster> monsterList;

	[HideInInspector] public List<int> spawnIndex;

	[HideInInspector] public List<Transform> spawnTransform;

	[HideInInspector] public bool[] isSpawned;
    [HideInInspector] public bool gameEnd = false;

	[HideInInspector] public int round;
	public int gold = 30;
    public int unitPurchaseCount = 0;

    public int[] percent = new int[3];

    public int amount_Upgrade1 = 0;
    public int amount_Upgrade2 = 0;
    public int amount_Upgrade3 = 0;

    public int cost_Upgrade1 = 3;
    public int cost_Upgrade2 = 5;
    public int cost_Upgrade3 = 7;
    
	public int indexLength = 0;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private Transform backgroundParent;

	private ObjectPoolManager _objectpoolManager;

	private IEnumerator waveCoroutine;

	/*----------------[PUBLIC METHOD]------------------------------*/

	public void GetGold(int value)
	{
		gold += value;
		//InGameUIManager.instance.panel_MainInGame.text_gold.text = "Gold : " + gold.ToString();
 
        InGameUIManager.instance.panel_MainInGame.text_gold.text = LanguageManager.instance.GetCurrentLanguageText("Text_Gold") + " : " + gold.ToString();
    }

    public bool CheckGold(int cost)
    {
        return cost <= gold;
    }

    public void EndGame()
    {
        if (gameEnd)
            return;
        gameEnd = true;

        StopCoroutine(waveCoroutine);
        int len = monsterList.Count;
        for(int i = 0; i < len; i++)
        {
            QuestMonster qm = monsterList[i] as QuestMonster;
            BossMonster bm = monsterList[i] as BossMonster;

            if(bm != null && qm != null)
            {
                Destroy(monsterList[i]);
            }
            else
                _objectpoolManager.ReturnMonster(monsterList[i]);
        }
        
        bool renewal = DataManager.instance.CheckRenewal(round);

        if (renewal)
        {
            UnlockAcheive();
            DataManager.instance.SaveIntData(DataManager.SaveDataType.UploadScrore, round);
        }

        //if (DataManager.instance.CheckData(DataManager.SaveDataType.UploadScrore))
        //{
        //    GooglePlayManager.instance.UploadRanking(
        //        GPGSIds.leaderboard,
        //        DataManager.instance.GetIntData(DataManager.SaveDataType.UploadScrore),
        //        () => { DataManager.instance.DeleteKey(DataManager.SaveDataType.UploadScrore);});
        //}
        
        InGameUIManager.instance.panel_Result.ShowResult(round, renewal);
    }

    public void MonsterUIRefresh()
    {
        int len = monsterList.Count;

        Panel_MainInGame mainUI = InGameUIManager.instance.panel_MainInGame;

        mainUI.text_monsterCount.text = len + " / " + 40;
        mainUI.image_monsterFill.fillAmount = (float)len / 40;
    }

    public void Restart()
    {
        InGameUIManager.instance.panel_Result.transform.DOScale(0, 0.25f).SetEase(Ease.InQuad).OnComplete(() => 
        {
            AdMobManager.instance.EventRelease();
            InGameUIManager.instance.panel_Result.Hide();
            MonsterUIRefresh();
            StartCoroutine(waveCoroutine);
        });
    }

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	protected override void Awake()
	{
		base.Awake();

        Time.timeScale = 1;

        round = 1;

        monsterList = new List<Monster>();
		spawnIndex = new List<int>();
		spawnTransform = new List<Transform>();

		Transform[] backgroundChildren = backgroundParent.GetComponentsInChildren<Transform>();
		int len = backgroundChildren.Length;
		indexLength = len - 1;
		isSpawned = new bool[len];

		for (int i = 1; i < len; i++)
		{
			spawnTransform.Add(backgroundChildren[i]);
			spawnIndex.Add(i - 1);
		}
	}

	private void Start()
	{
		if (_objectpoolManager == null)
			_objectpoolManager = ObjectPoolManager.instance;

        SoundManager.instance.PlaySound(SoundManager.AudioType.BGM, "BGM_InGame");

		waveCoroutine = MonsterWaveCoroutine();
		StartCoroutine(waveCoroutine);
    }

	private int RandomSpawnIndex(List<int> list_int)
	{
		int idx = Random.Range(0, list_int.Count);
		int returnValue = 0;

		returnValue = list_int[idx];
		list_int.Remove(list_int[idx]);

		return returnValue;
	}

    private void UnlockAcheive()
    {
        if (round >= 100)
        {
            GooglePlayManager.instance.GetAchievement(GPGSIds.achievement_100);
        }
        if (round >= 80)
        {
            GooglePlayManager.instance.GetAchievement(GPGSIds.achievement_80);
        }
        if (round >= 60)
        {
            GooglePlayManager.instance.GetAchievement(GPGSIds.achievement_60);
        }
        if (round >= 40)
        {
            GooglePlayManager.instance.GetAchievement(GPGSIds.achievement_40);
        }
        if (round >= 20)
        {
            GooglePlayManager.instance.GetAchievement(GPGSIds.achievement_20);
        }
    }

    private bool IsMonsterOver()
    {
        int len = monsterList.Count;

        return len > 40;
    }

	private IEnumerator MonsterWaveCoroutine()
	{
        yield return StartCoroutine(WaitWaveCoroutine(5));

        while (true)
		{
            //yield return StartCoroutine(WaitWaveCoroutine(30));

            InGameUIManager.instance.panel_MainInGame.text_time.text = round.ToString();

            if(round % 10 == 0)
            {
                BossMonster bossMonsterPrefab = ResourceManager.instance.GetMonoBehavioursObject<BossMonster>("BossMonster");
                BossMonster bossMonster = Instantiate(bossMonsterPrefab);
                bossMonster.transform.SetParent(null);
                roundCheckMonster.Add(bossMonster);
                monsterList.Add(bossMonster);

                MonsterUIRefresh();
                if (IsMonsterOver())
                {
                    EndGame();
                }
            }

            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Monster monster = _objectpoolManager.GetMonster();
                    monster.SetSpriteOrder(round - i);
                    monster.gameObject.SetActive(true);
                    roundCheckMonster.Add(monster);
                    monsterList.Add(monster);

                    MonsterUIRefresh();
                    if (IsMonsterOver())
                    {
                        EndGame();
                    }

                    yield return new WaitForSeconds(0.25f);
                }
            }			

			//yield return new WaitUntil(() => roundCheckMonster.Count <= 0);

			round++;

            yield return StartCoroutine(WaitWaveCoroutine(30));
        }
    }

    private IEnumerator WaitWaveCoroutine(int time)
    {
        UnityEngine.UI.Image timeImage = InGameUIManager.instance.panel_MainInGame.image_timeCheck;
        UnityEngine.UI.Text timeText = InGameUIManager.instance.panel_MainInGame.text_time;
        timeImage.fillAmount = 1;
        timeText.text = time.ToString();

        float checkTime = time;

        while(timeImage.fillAmount > 0)
        {
            timeImage.fillAmount -= Time.deltaTime / time;
            checkTime -= Time.deltaTime;
            timeText.text = ((int)checkTime + 1).ToString();

            yield return null;
        }
    }
}