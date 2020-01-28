/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-14 오후 11:37:32
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    /* [PUBLIC VARIABLE]					*/

    public Unit unit;

	public List<Monster> roundCheckMonster;
	public List<Monster> monsterList;

	[HideInInspector] public List<int> spawnIndex;

	[HideInInspector] public List<Transform> spawnTransform;

	[HideInInspector] public bool[] isSpawned;

	[HideInInspector] public int round;
	[HideInInspector] public int gold;

    public int[] percent = new int[3];

    public int amount_Upgrade1 = 0;
    public int amount_Upgrade2 = 0;
    public int amount_Upgrade3 = 0;

	public int indexLength = 0;

    [HideInInspector] public DataDefine.Attribute unitOneFirstAttribute = DataDefine.Attribute.None;
    [HideInInspector] public DataDefine.Attribute unitOneSecondAttribute = DataDefine.Attribute.None;
    [HideInInspector] public DataDefine.Attribute unitTwoFirstAttribute = DataDefine.Attribute.None;
    [HideInInspector] public DataDefine.Attribute unitTwoSecondAttribute = DataDefine.Attribute.None;
    [HideInInspector] public DataDefine.Attribute unitThreeFirstAttribute = DataDefine.Attribute.None;
    [HideInInspector] public DataDefine.Attribute unitThreeSecondAttribute = DataDefine.Attribute.None;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private Transform backgroundParent;

	private ObjectPoolManager _objectpoolManager;

	private IEnumerator waveCoroutine;

	/*----------------[PUBLIC METHOD]------------------------------*/

	public void GetGold(int value)
	{
		gold += value;
		InGameUIManager.instance.panel_MainInGame.text_gold.text = "Gold : " + gold.ToString();
	}

    public bool CheckGold(int cost)
    {
        return cost <= gold;
    }

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	protected override void Awake()
	{
		base.Awake();

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

	private IEnumerator MonsterWaveCoroutine()
	{
		round = 1;

		while (true)
		{
			int waveCount = round;

            if(waveCount % 10 == 0)
            {
                BossMonster bossMonster = ResourceManager.instance.GetMonoBehavioursObject<BossMonster>("BossMonster");
                Instantiate(bossMonster);
                bossMonster.transform.SetParent(null);
                roundCheckMonster.Add(bossMonster);
                monsterList.Add(bossMonster);
            }

            else
            {
                for (int i = 0; i < waveCount; i++)
                {
                    Monster monster = _objectpoolManager.GetMonster();
                    monster.gameObject.SetActive(true);
                    roundCheckMonster.Add(monster);
                    monsterList.Add(monster);
                    yield return new WaitForSeconds(0.2f);
                }
            }			

			yield return new WaitUntil(() => roundCheckMonster.Count <= 0);

			round++;

            yield return new WaitForSeconds(5f);

            Debug.Log("라운드 종료입니다!");
        }
	}
}