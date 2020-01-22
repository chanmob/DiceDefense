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

    public DataDefine.Attribute firstAttribue = DataDefine.Attribute.None;
    public DataDefine.Attribute secondAttribue = DataDefine.Attribute.None;


    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private Vector3? _clickPosition;
	private Vector3 diffMouseAndPosition;

	private ObjectPoolManager _objectpoolManager;
	private InGameManager _ingameManager;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void MoveToClickPosition(Vector3 pos)
    {
        _clickPosition = pos;
    }

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	private void OnMouseDown()
	{
		diffMouseAndPosition = transform.position - Input.mousePosition;	
	}

	private void OnMouseDrag()
	{
		transform.position = Input.mousePosition + diffMouseAndPosition;
	}

	private void OnMouseUp()
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

		if (idx != -1 && distance <= 1000)
		{
			if (_ingameManager.isSpawned[idx] == false)
			{
				transform.position = transformList[idx].transform.position;
			}
			else
			{
				Unit temp = UnitManager.instance.unitArray[idx];
				transform.position = transformList[idx].transform.position;
				temp.transform.position = transformList[unitPositionIndex].transform.position;

				temp.unitPositionIndex = unitPositionIndex;
				unitPositionIndex = idx;
			}
		}
		else
		{
			transform.position = _ingameManager.spawnTransform[unitPositionIndex].position;
		}
	}

	private void Start()
	{
		if(_objectpoolManager == null)
			_objectpoolManager = ObjectPoolManager.instance;

		if (_ingameManager == null)
			_ingameManager = InGameManager.instance;

		InvokeRepeating("Attack", 0f, attackSpeed);
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