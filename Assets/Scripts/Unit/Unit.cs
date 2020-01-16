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
    public int power;
    public float attackRange;

    public DataDefine.Attribute firstAttribue = DataDefine.Attribute.None;
    public DataDefine.Attribute secondAttribue = DataDefine.Attribute.None;


    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private Vector3? _clickPosition;

	private ObjectPoolManager _objectpoolManager;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void MoveToClickPosition(Vector3 pos)
    {
        _clickPosition = pos;
    }

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	private void Start()
	{
		if(_objectpoolManager == null)
			_objectpoolManager = ObjectPoolManager.instance;

		//InvokeRepeating("Attack", 0f, attackSpeed);
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
		List<Monster> monsters = InGameManager.instance.monsterList;
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