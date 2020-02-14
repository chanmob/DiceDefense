﻿/*
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
	private DataDefine.Attribute _secondAttribute = DataDefine.Attribute.None;

	/*----------------[PUBLIC METHOD]------------------------------*/

	public void SetTarget(Monster monster)
	{
		_targetMonster = monster;
	}

	public void SetAttribute(DataDefine.Attribute first = DataDefine.Attribute.None, DataDefine.Attribute second = DataDefine.Attribute.None)
	{
		_firstAttribute = first;
		_secondAttribute = second;
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
            float secondAttributeMulti = 1f;

			if (_firstAttribute != DataDefine.Attribute.None)
			{
				switch (_firstAttribute)
				{
					case DataDefine.Attribute.Cloud:
                        firstAttributeMulti = _targetMonster.CheckFirstAttribute(DataDefine.Attribute.Fire, DataDefine.Attribute.Mountain);
						break;
					case DataDefine.Attribute.Fire:
                        firstAttributeMulti = _targetMonster.CheckFirstAttribute(DataDefine.Attribute.Water, DataDefine.Attribute.Cloud);
                        break;
					case DataDefine.Attribute.Water:
                        firstAttributeMulti = _targetMonster.CheckFirstAttribute(DataDefine.Attribute.Mountain, DataDefine.Attribute.Fire);
                        break;
					case DataDefine.Attribute.Mountain:
                        firstAttributeMulti = _targetMonster.CheckFirstAttribute(DataDefine.Attribute.Cloud, DataDefine.Attribute.Water);
                        break;
				}			
			}
			if (_secondAttribute != DataDefine.Attribute.None)
			{
                switch (_secondAttribute)
                {
                    case DataDefine.Attribute.Second1:
                        secondAttributeMulti = _targetMonster.CheckSecondAttribute(DataDefine.Attribute.Second2, DataDefine.Attribute.Second4);
                        break;
                    case DataDefine.Attribute.Second2:
                        secondAttributeMulti = _targetMonster.CheckSecondAttribute(DataDefine.Attribute.Second3, DataDefine.Attribute.Second1);
                        break;
                    case DataDefine.Attribute.Second3:
                        secondAttributeMulti = _targetMonster.CheckSecondAttribute(DataDefine.Attribute.Second4, DataDefine.Attribute.Second2);
                        break;
                    case DataDefine.Attribute.Second4:
                        secondAttributeMulti = _targetMonster.CheckSecondAttribute(DataDefine.Attribute.Second1, DataDefine.Attribute.Second3);
                        break;
                }
			}

            power = (int)(power * firstAttributeMulti * secondAttributeMulti);
            _targetMonster.Hit(power);
			_objectpoolManager.ReturnBullet(this);
        }
	}
}