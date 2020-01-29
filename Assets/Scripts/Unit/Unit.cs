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

    public float attackSpeed;
	public float attackRange;

	public int power;
	public int unitPositionIndex;
    public int unitLevel = 0;

    public DataDefine.Attribute firstAttribue = DataDefine.Attribute.None;
    public DataDefine.Attribute secondAttribue = DataDefine.Attribute.None;


    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private Transform _diceCount;
    private Transform _diceAttribute;

    private Vector3? _clickPosition;
	private Vector3 _diffMouseAndPosition;
    private Vector3 _unitPosition;

    private ObjectPoolManager _objectpoolManager;
	private InGameManager _ingameManager;

    private Animator _animator;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void MoveToClickPosition(Vector3 pos)
    {
        _clickPosition = pos;
    }

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

        power = power + unitLevel;
        gameObject.name = gameObject.name + "_" + unitLevel;
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

            if(sqrDistance < unitDistance)
            {
                unitIndex = i;
                unitDistance = sqrDistance;
            }
        }

        if (unitIndex != -1 && unitDistance <= 0.15f)
        {
            if(string.Equals(gameObject.name, unitManager.unitList[unitIndex].name))
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

	private void Start()
	{
		if(_objectpoolManager == null)
			_objectpoolManager = ObjectPoolManager.instance;

		if (_ingameManager == null)
			_ingameManager = InGameManager.instance;

        _animator = GetComponent<Animator>();
        _diceCount = transform.Find("DiceCount");
        _diceAttribute = transform.Find("Attribute");

		InvokeRepeating("Attack", 0f, attackSpeed);

        _unitPosition = transform.position;
	}

	private void Update()
    {
        if(_clickPosition != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)_clickPosition, 2 * Time.deltaTime);

            if(Vector2.Distance(transform.position, (Vector2)_clickPosition) <= 0.025f)
            {
                transform.position = (Vector2)_clickPosition;
                _clickPosition = null;
            }
        }
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
		bullet.power = power;
		bullet.SetTarget(targetMonster);
		bullet.gameObject.SetActive(true);
	}
}