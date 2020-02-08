/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-14 오후 11:09:53
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    /* [PUBLIC VARIABLE]					*/

    public string unitName;

    public float attackSpeed;
	public float attackRange;

	public int power;
	public int unitPositionIndex;
    public int unitLevel = 0;

    public DataDefine.Attribute firstAttribue = DataDefine.Attribute.None;
    public DataDefine.Attribute secondAttribue = DataDefine.Attribute.None;

    public DataDefine.UnitType unitType = DataDefine.UnitType.None;

    public DataDefine.UnitRanking unitRanking = DataDefine.UnitRanking.None;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private int _startPower;

    private Transform _diceCount;
    private Transform _diceAttribute;

	private Vector3 _diffMouseAndPosition;
    private Vector3 _unitPosition;

    private ObjectPoolManager _objectpoolManager;
	private InGameManager _ingameManager;

    private Animator _animator;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void UnitLevelUp()
    {
        unitLevel++;
         
        int len = _diceCount.childCount;

        for(int i = 0; i < len; i++)
        {
            if (_diceCount.GetChild(i).gameObject.activeSelf == true)
                _diceCount.GetChild(i).gameObject.SetActive(false);
        }

        _diceCount.GetChild(unitLevel).gameObject.SetActive(true);

        power = _startPower + (int)Mathf.Pow(unitLevel, 2) * 2 + 1;
        //power = power + unitLevel;
        gameObject.name = gameObject.name + "_" + unitLevel;

        if(firstAttribue == DataDefine.Attribute.None && unitLevel >= 2)
        {
            InGameUIManager.instance.panel_UnitAttribute.isSecondAttribute = false;
            InGameUIManager.instance.panel_UnitAttribute.Show();
        }

        if(secondAttribue == DataDefine.Attribute.None && unitLevel >= 3)
        {
            InGameUIManager.instance.panel_UnitAttribute.isSecondAttribute = true;
            InGameUIManager.instance.panel_UnitAttribute.Show();
        }
    }

    public void SetPosition(Vector3 pos)
    {
        _unitPosition = pos;
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private void OnDisable()
    {
        UnitManager.instance.unitList.Remove(this);
        CancelInvoke();
    }

    private void OnMouseDown()
	{
        _diffMouseAndPosition = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);	
	}

	private void OnMouseDrag()
	{
		transform.position = _diffMouseAndPosition + Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private void OnMouseUp()
	{
        Vector2 mouseOffset = _unitPosition - transform.position;
        float mouseDistance = mouseOffset.sqrMagnitude;

        if(mouseDistance <= 0.1f)
        {
            Debug.Log("유닛 정보 표시");
            transform.position = _unitPosition;
            InGameUIManager.instance.popup_UnitInfo.ShowUnitInfo(this);
        }

        else
        {
            UnitManager unitManager = UnitManager.instance;
            int cnt = unitManager.unitList.Count;
            int unitIndex = -1;
            float unitDistance = Mathf.Infinity;
            for (int i = 0; i < cnt; i++)
            {
                if (unitManager.unitList[i] == this)
                    continue;

                Vector2 offset = unitManager.unitList[i].transform.position - transform.position;
                float sqrDistance = offset.sqrMagnitude;

                if (sqrDistance < unitDistance)
                {
                    unitIndex = i;
                    unitDistance = sqrDistance;
                }
            }

            if (unitIndex != -1 && unitDistance <= 0.15f && unitLevel < 5)
            {
                if (string.Equals(gameObject.name, unitManager.unitList[unitIndex].name))
                {
                    unitManager.unitList[unitIndex].UnitLevelUp();
                    _ingameManager.isSpawned[unitPositionIndex] = false;
                    _ingameManager.spawnIndex.Add(unitPositionIndex);
                    gameObject.SetActive(false);
                }
                else
                {
                    Unit temp = UnitManager.instance.unitList[unitIndex];
                    int idx = temp.unitPositionIndex;
                    transform.position = temp.transform.position;
                    temp.transform.position = _unitPosition;
                    temp.unitPositionIndex = unitPositionIndex;
                    unitPositionIndex = idx;
                    SetPosition(transform.position);
                    temp.SetPosition(temp.transform.position);
                }
            }

            else
            {
                List<Transform> transformList = _ingameManager.spawnTransform;

                int len = transformList.Count;
                float distance = Mathf.Infinity;
                int idx = -1;

                for (int i = 0; i < len; i++)
                {
                    Vector2 offset = transformList[i].transform.position - transform.position;
                    float sqrDistance = offset.sqrMagnitude;

                    if (sqrDistance < distance)
                    {
                        idx = i;
                        distance = sqrDistance;
                    }
                }

                if (idx != -1 && distance <= 0.25f)
                {
                    if (_ingameManager.isSpawned[idx] == false)
                    {
                        transform.position = transformList[idx].transform.position;
                        _ingameManager.isSpawned[unitPositionIndex] = false;
                        _ingameManager.isSpawned[idx] = true;
                        _ingameManager.spawnIndex.Add(unitPositionIndex);
                        _ingameManager.spawnIndex.Remove(idx);
                        SetPosition(transform.position);

                        unitPositionIndex = idx;
                    }
                    else
                    {
                        Unit temp = UnitManager.instance.unitArray[idx];
                        transform.position = transformList[idx].transform.position;
                        temp.transform.position = transformList[unitPositionIndex].transform.position;
                        temp.unitPositionIndex = unitPositionIndex;
                        unitPositionIndex = idx;
                        SetPosition(transform.position);
                        temp.SetPosition(temp.transform.position);
                    }
                }
                else
                {
                    transform.position = _unitPosition;
                }
            }
        }        
	}

	protected void Start()
	{
		if(_objectpoolManager == null)
			_objectpoolManager = ObjectPoolManager.instance;

		if (_ingameManager == null)
			_ingameManager = InGameManager.instance;

        _animator = GetComponent<Animator>();
        _diceCount = transform.Find("DiceCount");
        _diceAttribute = transform.Find("Attribute");

        _unitPosition = transform.position;

        OnStart();

        _startPower = power;
        InvokeRepeating("Attack", 0f, attackSpeed);
	}

    protected virtual void OnStart()
    {

    }

	private GameObject FindMonster()
	{
		List<Monster> monsters = _ingameManager.monsterList;
		int len = monsters.Count;
		float distance = Mathf.Infinity;

		GameObject targetMonster = null;

		for(int i = 0; i < len; i++)
		{
			Vector2 offset = monsters[i].transform.position - transform.position;
			float sqrDistance = offset.sqrMagnitude;

			if(sqrDistance < distance)
			{
				targetMonster = monsters[i].gameObject;
				distance = sqrDistance;
			}
		}

		return targetMonster;
	}

	private void Attack()
	{
		GameObject target = FindMonster();

		if (target == null)
			return;

		Monster targetMonster = target.GetComponent<Monster>();

		Bullet bullet = _objectpoolManager.GetBullet();
		bullet.transform.position = transform.position;

        switch (unitType)
        {
            case DataDefine.UnitType.Unit1:
                bullet.power = power + _ingameManager.amount_Upgrade1;
                break;
            case DataDefine.UnitType.Unit2:
                bullet.power = power + _ingameManager.amount_Upgrade2;
                break;
            case DataDefine.UnitType.Unit3:
                bullet.power = power + _ingameManager.amount_Upgrade3;
                break;
        }

        bullet.SetAttribute(firstAttribue, secondAttribue);
        //bullet.power = power;
		bullet.SetTarget(targetMonster);
		bullet.gameObject.SetActive(true);
	}
}