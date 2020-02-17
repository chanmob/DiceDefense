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

    public DataDefine.Attribute firstAttribute = DataDefine.Attribute.None;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _edgeRenderer;

    private Canvas _canvas;

    private ObjectPoolManager _objectpoolManager;
	protected InGameManager _ingameManager;

    private Transform _waypoint;
    protected Transform _shieldTransform;

    [SerializeField]private Text _text_HP;

    protected int _hp;
    protected int _shield;
    protected int _startShieldAmount;
    private int _waypointIndex;

	private bool _isDie;

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void SetSpriteOrder(int layer)
    {
        _spriteRenderer.sortingOrder = layer;
        _edgeRenderer.sortingOrder = layer + 1;
        _canvas.sortingOrder = layer + 2;
    }

    public void Hit(int damage)
    {
		DecreaseHP(damage);

        if (_hp <= 0 && _isDie == false)
        {
			_isDie = true;
            Die();
        }
    }

	public float CheckFirstAttribute(DataDefine.Attribute strength, DataDefine.Attribute weakness)
	{
		if(firstAttribute == strength)
		{
			return 2;
		}
		else if(firstAttribute == weakness)
		{
            return 0.5f;
		}
		else
		{
            return 1;
		}
	}

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	private void OnEnable()
    {
        if (_ingameManager == null)
            _ingameManager = InGameManager.instance;

        Enable();
        PublicEnable();
    }

    protected virtual void Enable()
    {
        SetHP(_ingameManager.round * ((int)(_ingameManager.round * 0.1f) + 1) * 10);
	}

    private void PublicEnable()
    {
        _isDie = false;

        SetPositionFirstWaypoint();

        if (_shield > 0)
        {
            _startShieldAmount = _shield;
            _shieldTransform.localScale = new Vector3(1 + (_shield / (float)_startShieldAmount) * 0.5f, 1 + (_shield / (float)_startShieldAmount) * 0.5f, 1);
        }
        else
            _shieldTransform.gameObject.SetActive(false);
    }

	private void OnDisable()
	{
		Disable();
	}

	protected virtual void Disable()
	{
        if (_isDie == false)
            return;

        transform.position = _waypoint.position;
        _ingameManager.GetGold(10);
    }

    protected virtual void Die()
    {
        _ingameManager.monsterList.Remove(this);
		_ingameManager.roundCheckMonster.Remove(this);
        _objectpoolManager.ReturnMonster(this);
    }

	private void Awake()
	{
		_text_HP = GetComponentInChildren<Text>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _canvas = GetComponentInChildren<Canvas>();

        Transform rainbow = transform.Find("RainBow");
        if (rainbow != null)
            _edgeRenderer = rainbow.GetComponent<SpriteRenderer>();        

        _shieldTransform = transform.Find("Shield");
	}

    private void SetPositionFirstWaypoint()
    {
        _waypointIndex = 0;
        _waypoint = WayPointManager.instance.waypoints[_waypointIndex];
        transform.position = _waypoint.position;
    }

	private void Start()
    {
        SetPositionFirstWaypoint();

		if(_ingameManager == null)
			_ingameManager = InGameManager.instance;

        if (_objectpoolManager == null)
            _objectpoolManager = ObjectPoolManager.instance;
    }

    private void FixedUpdate()
    {
        if (_ingameManager.gameEnd == true)
            return;

        transform.position = Vector2.MoveTowards(transform.position, _waypoint.position, speed * Time.deltaTime);        

        if(Vector2.Distance(transform.position, _waypoint.position) <= 0.025f)
        {
            transform.position = _waypoint.position;

            if (_waypointIndex < WayPointManager.instance.waypointLength - 1)
            {
                _waypointIndex++;
                _waypoint = _waypoint = WayPointManager.instance.waypoints[_waypointIndex];
            }

            else
            {
                Debug.Log("게임 종료");
                _ingameManager.EndGame();
            }
        }
    }

	protected void SetHP(int hp)
	{
		_hp = hp;
		_text_HP.text = _hp.ToString();
	}

	private void DecreaseHP(int damage)
	{
        if(_shield > 0)
        {
            _shield--;
            _shieldTransform.localScale = new Vector3(1 + (_shield / (float)_startShieldAmount) * 0.5f, 1 + (_shield / (float)_startShieldAmount) * 0.5f, 1);
            return;
        }

		_hp -= damage;
		_text_HP.text = _hp.ToString();
	}
}