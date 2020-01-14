/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-14 오후 10:41:27
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : Singleton<WayPointManager>
{
    /* [PUBLIC VARIABLE]					*/

    public Transform[] waypoints {
        get {
                return _waypoints;
        }
    }

    public int waypointLength;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private Transform[] _waypoints;

    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void Awake()
    {
        base.Awake();

        waypointLength = _waypoints.Length;
    }
}