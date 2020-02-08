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
    public Unit[] superUnits;
    public Unit[] hiddenUnits;

	public Unit[] unitArray;

    public List<Unit> unitList;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private Dictionary<string, int> _unitDictionary;

	private InGameManager _ingameManager;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void SetUnitFirstAttribute(DataDefine.UnitType type, DataDefine.Attribute attribute)
    {
        switch (type)
        {
            case DataDefine.UnitType.Unit1:
                _ingameManager.unitOneFirstAttribute = attribute;
                break;
            case DataDefine.UnitType.Unit2:
                _ingameManager.unitTwoFirstAttribute = attribute;
                break;
            case DataDefine.UnitType.Unit3:
                _ingameManager.unitThreeFirstAttribute = attribute;
                break;
        }

        int len = unitList.Count;
        for (int i = 0; i < len; i++)
        {
            if(unitList[i].unitType == type)
            {
                unitList[i].firstAttribue = attribute;
            }
        }
    }

    public void SetUnitSecondAttribute(DataDefine.UnitType type, DataDefine.Attribute attribute)
    {
        switch (type)
        {
            case DataDefine.UnitType.Unit1:
                _ingameManager.unitOneSecondAttribute = attribute;
                break;
            case DataDefine.UnitType.Unit2:
                _ingameManager.unitTwoSecondAttribute = attribute;
                break;
            case DataDefine.UnitType.Unit3:
                _ingameManager.unitThreeSecondAttribute = attribute;
                break;
        }

        int len = unitList.Count;
        for (int i = 0; i < len; i++)
        {
            if (unitList[i].unitType == type)
            {
                unitList[i].secondAttribue = attribute;
            }
        }
    }

    public void BuyUnit()
	{
        int len = _ingameManager.spawnIndex.Count;
        if (len == 0)
            return;

		int index = RandomSpawnIndex(_ingameManager.spawnIndex);

		int rank = GetPercent();
		int division = Random.Range(0, 3);

		string name = units[division].name;

        Unit unitObject = null;

        switch (rank)
        {
            case 0:
                unitObject = Instantiate(units[division], _ingameManager.spawnTransform[index].position, Quaternion.identity);
                break;
            case 1:
                unitObject = Instantiate(superUnits[division], _ingameManager.spawnTransform[index].position, Quaternion.identity);
                break;
            case 2:
                unitObject = Instantiate(hiddenUnits[division], _ingameManager.spawnTransform[index].position, Quaternion.identity);
                break;
        }

        //Unit unitObject = Instantiate(units[division], _ingameManager.spawnTransform[index].position, Quaternion.identity);
        //unitObject.name = units[division].name;
        unitList.Add(unitObject);

		unitArray[index] = unitObject;
        unitObject.unitPositionIndex = index;

        switch (division)
        {
            case 0:
                if(_ingameManager.unitOneFirstAttribute != DataDefine.Attribute.None)
                    unitObject.firstAttribue = _ingameManager.unitOneFirstAttribute;

                if(_ingameManager.unitOneSecondAttribute != DataDefine.Attribute.None)
                    unitObject.secondAttribue = _ingameManager.unitOneSecondAttribute;
                break;
            case 1:
                if (_ingameManager.unitTwoFirstAttribute != DataDefine.Attribute.None)
                    unitObject.firstAttribue = _ingameManager.unitTwoFirstAttribute;

                if (_ingameManager.unitTwoSecondAttribute != DataDefine.Attribute.None)
                    unitObject.secondAttribue = _ingameManager.unitTwoSecondAttribute;
                break;
            case 2:
                if (_ingameManager.unitThreeFirstAttribute != DataDefine.Attribute.None)
                    unitObject.firstAttribue = _ingameManager.unitThreeFirstAttribute;

                if (_ingameManager.unitThreeSecondAttribute != DataDefine.Attribute.None)
                    unitObject.secondAttribue = _ingameManager.unitThreeSecondAttribute;
                break;
        }

        _ingameManager.isSpawned[index] = true;
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
                break;
			}
		}

		return returnValue;
	}

	protected override void Awake()
	{
		base.Awake();

		_unitDictionary = new Dictionary<string, int>();
        unitList = new List<Unit>();
	}

	private void Start()
	{
		if (_ingameManager == null)
			_ingameManager = InGameManager.instance;

		unitArray = new Unit[_ingameManager.indexLength];
	}
}