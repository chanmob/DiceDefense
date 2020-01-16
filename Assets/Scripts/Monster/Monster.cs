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

	private InGameManager _ingameManager;

    private Transform _waypoint;

    [SerializeField]private Text _text_HP;

    private int _hp;
    private int _waypointIndex;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void Hit(int damage)
    {
		DecreaseHP(damage);

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
		if (_ingameManager == null)
			_ingameManager = InGameManager.instance;

		SetHP(_ingameManager.round * ((int)(_ingameManager.round * 0.1f) + 1) * 10);
	}

	private void OnDisable()
	{
		Disable();
	}

	protected virtual void Disable()
	{

	}

	protected virtual void Die()
    {
		_ingameManager.monsterList.Remove(this);
    }

	private void Awake()
	{
		_text_HP = GetComponentInChildren<Text>();
	}

	private void Start()
    {
        _waypointIndex = 0;
        _waypoint = WayPointManager.instance.waypoints[_waypointIndex];

		if(_ingameManager == null)
			_ingameManager = InGameManager.instance;
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

	private void SetHP(int hp)
	{
		_hp = hp;
		_text_HP.text = _hp.ToString();
	}

	private void DecreaseHP(int damage)
	{
		_hp -= damage;
		_text_HP.text = _hp.ToString();
	}
}