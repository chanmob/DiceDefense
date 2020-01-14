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
    public float power;
    public float attackRange;

    public DataDefine.Attribute firstAttribue = DataDefine.Attribute.None;
    public DataDefine.Attribute secondAttribue = DataDefine.Attribute.None;


    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private Vector3? _clickPosition;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void MoveToClickPosition(Vector3 pos)
    {
        _clickPosition = pos;
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

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
}