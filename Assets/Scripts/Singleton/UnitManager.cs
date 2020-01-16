/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-14 오후 11:32:34
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    /* [PUBLIC VARIABLE]					*/

    public Unit[] units;
    public Unit[] hiddenUnits;

	/* [PROTECTED && PRIVATE VARIABLE]		*/

	private Dictionary<string, int> unitDictionary;

	private InGameManager _ingameManager;

	/*----------------[PUBLIC METHOD]------------------------------*/

	public void BuyUnit()
	{
		int index = RandomSpawnIndex(_ingameManager.spawnIndex);

		int rank = GetPercent();
		int division = Random.Range(0, 3);

		Instantiate(units[division], _ingameManager.spawnTransform[index].position, Quaternion.identity);

		//switch (rank)
		//{
		//    case 0:
		//        Instantiate(UnitManager.instance.units[division], spawnTransform[index].position, Quaternion.identity);
		//        break;
		//    case 1:
		//        Instantiate(UnitManager.instance.units[division], spawnTransform[index].position, Quaternion.identity);
		//        break;
		//    case 2:
		//        Instantiate(UnitManager.instance.hiddenUnits[division], spawnTransform[index].position, Quaternion.identity);
		//        break;
		//}

		_ingameManager.isSpawned[index] = true;
	}


	public void AddUnit(Unit unit)
	{
		string unitName = unit.gameObject.name;

		if (unitDictionary.ContainsKey(unitName) == false)
		{
			unitDictionary.Add(unitName, 0);
		}

		unitDictionary[unitName]++;
	}

	public void SubUnit(Unit unit)
	{
		string unitName = unit.gameObject.name;

		if (unitDictionary.ContainsKey(unitName) == false)
			return;

		unitDictionary[unitName]--;
	}

	public int CheckUnitCount(string name)
	{
		if (unitDictionary.ContainsKey(name) == false)
			return 0;

		return unitDictionary[name];
	}

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	private int RandomSpawnIndex(List<int> list_int)
	{
		int idx = Random.Range(0, list_int.Count);
		int returnValue = 0;

		returnValue = list_int[idx];
		list_int.Remove(list_int[idx]);

		return returnValue;
	}

	/// <summary>
	/// 0 - 노말, 1 - 슈퍼, 2- 히든
	/// </summary>
	/// <returns></returns>
	private int GetPercent()
	{
		int len = _ingameManager.percent.Length;
		int sum = 0;
		int returnValue = 0;

		for (int i = 0; i < len; i++)
		{
			sum += _ingameManager.percent[i];
		}

		int ran = Random.Range(0, sum);

		for (int i = 0; i < len; i++)
		{
			ran -= _ingameManager.percent[i];

			if (ran <= 0)
			{
				returnValue = i;
			}
		}

		return returnValue;
	}

	protected override void Awake()
	{
		base.Awake();

		unitDictionary = new Dictionary<string, int>();
	}

	private void Start()
	{
		if (_ingameManager == null)
			_ingameManager = InGameManager.instance;
	}
}