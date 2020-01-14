/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-14 오후 10:41:12
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    /* [PUBLIC VARIABLE]					*/

    public float speed;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private Transform _waypoint;

    private Text _text;

    private int _hp;
    private int _waypointIndex;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void Hit(int damage)
    {
        _hp -= damage;
        _text.text = _hp.ToString();

        if (_hp <= 0)
        {
            Die();
        }
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private void OnEnable()
    {
        Enable();
    }

    protected virtual void Enable()
    {

    }

    protected virtual void Die()
    {

    }

    private void Start()
    {
        _text = GetComponentInChildren<Text>();
        _waypointIndex = 0;
        _waypoint = WayPointManager.instance.waypoints[_waypointIndex];
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, _waypoint.position, speed * Time.deltaTime);        

        if(Vector2.Distance(transform.position, _waypoint.position) <= 0.025f)
        {
            transform.position = _waypoint.position;

            if (_waypointIndex < WayPointManager.instance.waypointLength - 1)
            {
                _waypointIndex++;
                _waypoint = _waypoint = WayPointManager.instance.waypoints[_waypointIndex];
            }
        }
    }
}