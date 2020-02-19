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
	public int power;

	/* [PROTECTED && PRIVATE VARIABLE]		*/

	private Monster _targetMonster;
	private ObjectPoolManager _objectpoolManager;

	private DataDefine.Attribute _firstAttribute = DataDefine.Attribute.None;

	/*----------------[PUBLIC METHOD]------------------------------*/

	public void SetTarget(Monster monster)
	{
		_targetMonster = monster;
	}

	public void SetAttribute(DataDefine.Attribute first = DataDefine.Attribute.None)
	{
		_firstAttribute = first;
	}

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	private void Start()
	{
		_objectpoolManager = ObjectPoolManager.instance;
	}

	private void FixedUpdate()
	{
		if(_targetMonster == null)
		{
			_objectpoolManager.ReturnBullet(this);
		}
		else
		{
			if (_targetMonster.gameObject.activeSelf)
			{
				transform.position = Vector2.MoveTowards(transform.position, _targetMonster.transform.position, speed * Time.deltaTime);
			}
			else
			{
				_objectpoolManager.ReturnBullet(this);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject == _targetMonster.gameObject)
		{
            float firstAttributeMulti = 1f;

			if (_firstAttribute != DataDefine.Attribute.None)
			{
				switch (_firstAttribute)
				{
					case DataDefine.Attribute.Cloud:
                        firstAttributeMulti = _targetMonster.CheckFirstAttribute(DataDefine.Attribute.Infernal, DataDefine.Attribute.Mountain);
						break;
					case DataDefine.Attribute.Infernal:
                        firstAttributeMulti = _targetMonster.CheckFirstAttribute(DataDefine.Attribute.Ocean, DataDefine.Attribute.Cloud);
                        break;
					case DataDefine.Attribute.Ocean:
                        firstAttributeMulti = _targetMonster.CheckFirstAttribute(DataDefine.Attribute.Mountain, DataDefine.Attribute.Infernal);
                        break;
					case DataDefine.Attribute.Mountain:
                        firstAttributeMulti = _targetMonster.CheckFirstAttribute(DataDefine.Attribute.Cloud, DataDefine.Attribute.Ocean);
                        break;
				}			
			}

            power = (int)(power * firstAttributeMulti);
            _targetMonster.Hit(power);
			_objectpoolManager.ReturnBullet(this);
        }
	}
}