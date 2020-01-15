/*
  ============================================
	Author	: user
	Time 	: 2020-01-14 오후 10:47:29
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    /* [PUBLIC VARIABLE]					*/

    public Stack<Monster> stack_Monster;
	public Stack<Bullet> stack_Bullet;

	/* [PROTECTED && PRIVATE VARIABLE]		*/

	[SerializeField] private GameObject monsterPoolParent;
	[SerializeField] private GameObject bulletPoolParent;

	/*----------------[PUBLIC METHOD]------------------------------*/

	public Monster GetMonster()
	{
		Monster newMonster = null;

		int len = stack_Monster.Count;

		if(len == 0)
		{

		}
		else
		{
			newMonster = stack_Monster.Pop();
		}

		return newMonster;
	}

	public void ReturnMonster(Monster monster)
	{
		if (monster.gameObject.activeSelf)
			monster.gameObject.SetActive(false);

		stack_Monster.Push(monster);
	}

	public Bullet GetBullet()
	{
		Bullet newBullet = null;

		int len = stack_Bullet.Count;

		if(len == 0)
		{

		}
		else
		{
			newBullet = stack_Bullet.Pop();
		}

		return newBullet;
	}

	public void ReturnBullet(Bullet bullet)
	{
		if (bullet.gameObject.activeSelf)
			bullet.gameObject.SetActive(false);

		stack_Bullet.Push(bullet);
	}

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	protected override void Awake()
    {
        base.Awake();

        stack_Monster = new Stack<Monster>();
    }

    private void MakeMonsterPool(int count)
    {
		Monster monsterPrefab = ResourceManager.instance.GetObject<Monster>("");

        for(int i = 0; i < count; i++)
        {

        }
    }

	private void MakeBulletPool(int count)
	{
		Bullet bulletPrefab = ResourceManager.instance.GetObject<Bullet>("");

		for(int i = 0; i < count; i++)
		{

		}
	}
}