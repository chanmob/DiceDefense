/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-15 오후 4:58:53
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	/* [PUBLIC VARIABLE]					*/

	public float speed;

	/* [PROTECTED && PRIVATE VARIABLE]		*/

	private Monster _targetMonster;

	/*----------------[PUBLIC METHOD]------------------------------*/

	public void SetTarget(Monster monster)
	{
		_targetMonster = monster;
	}

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	private void FixedUpdate()
	{
		if(_targetMonster == null)
		{
			ObjectPoolManager.instance.ReturnBullet(this);
		}
		else
		{
			if (_targetMonster.gameObject.activeSelf)
			{
				transform.position = Vector2.MoveTowards(transform.position, _targetMonster.transform.position, speed * Time.deltaTime);
			}
			else
			{
				ObjectPoolManager.instance.ReturnBullet(this);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject == _targetMonster.gameObject)
		{

		}
	}
}