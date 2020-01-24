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

	public Unit[] unitArray;

	/* [PROTECTED && PRIVATE VARIABLE]		*/

	private Dictionary<string, int> _unitDictionary;

	private List<Unit> _unitList;

	private InGameManager _ingameManager;

	/*----------------[PUBLIC METHOD]------------------------------*/

	public void BuyUnit()
	{
		int index = RandomSpawnIndex(_ingameManager.spawnIndex);

		int rank = GetPercent();
		int division = Random.Range(0, 3);

		string name = units[division].name;

        Unit unitObject = Instantiate(units[division], _ingameManager.spawnTransform[index].position, Quaternion.identity);
        AddUnit(unitObject);

        if (CheckUnitCount(name) >= 2)
		{
            unitObject.gameObject.SetActive(false);
			UnitCheck(name);
            Debug.Log("Level Up");
		}

		unitArray[index] = unitObject;
        _unitList.Add(unitObject);
        unitObject.unitPositionIndex = index;
        unitObject.gameObject.SetActive(true);

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

		if (_unitDictionary.ContainsKey(unitName) == false)
		{
			_unitDictionary.Add(unitName, 0);
		}

		_unitDictionary[unitName]++;
	}

	public void SubUnit(Unit unit)
	{
		string unitName = unit.gameObject.name;

		if (_unitDictionary.ContainsKey(unitName) == false)
			return;

		_unitDictionary[unitName]--;
	}

	public int CheckUnitCount(string name)
	{
		if (_unitDictionary.ContainsKey(name) == false)
			return 0;

		return _unitDictionary[name];
	}

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	private void UnitCheck(string unitName)
	{
		if (CheckUnitCount(unitName) < 2 || string.IsNullOrEmpty(unitName) == true)
			return;

		int len = _unitList.Count;
        bool upgraded = false;
		string upgradeUnitName = string.Empty;

		for(int i = 0; i < len; i++)
		{
			Unit unit = _unitList[i];

			if(string.Equals(unitName, unitName) == true && unit.gameObject.activeSelf == true)
			{
                if (upgraded)
                {
                    _unitList.Remove(unit);
                    unit.gameObject.SetActive(false);
                }
                else
                {
                    GameObject upgradeUnit = unit.gameObject;
                    //upgradeUnit.name = "Upgraded";
                    upgradeUnitName = upgradeUnit.name;
                    upgraded = true;
                }
            }
		}

		UnitCheck(upgradeUnitName);
	}

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

		_unitDictionary = new Dictionary<string, int>();
        _unitList = new List<Unit>();
	}

	private void Start()
	{
		if (_ingameManager == null)
			_ingameManager = InGameManager.instance;

		unitArray = new Unit[_ingameManager.indexLength];
	}
}