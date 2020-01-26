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
			Monster monsterPrefab = ResourceManager.instance.GetMonoBehavioursObject<Monster>("Monster");
			Monster monster = Instantiate(monsterPrefab);
			monster.transform.SetParent(monsterPoolParent.transform);
			monster.gameObject.SetActive(false);

			newMonster = monster;
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
			Bullet bulletPrefab = ResourceManager.instance.GetMonoBehavioursObject<Bullet>("Bullet");
			Bullet bullet = Instantiate(bulletPrefab);
			bullet.transform.SetParent(bulletPoolParent.transform);
			bullet.gameObject.SetActive(false);

			newBullet = bullet;
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
        {
            bullet.gameObject.SetActive(false);
        }

		stack_Bullet.Push(bullet);
	}

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	protected override void Awake()
    {
        base.Awake();

        stack_Monster = new Stack<Monster>();
		stack_Bullet = new Stack<Bullet>();
    }

	private void Start()
	{
		MakeMonsterPool(10);
        MakeBulletPool(10);
	}

	private void MakeMonsterPool(int count)
    {
		Monster monsterPrefab = ResourceManager.instance.GetMonoBehavioursObject<Monster>("Monster");

        for(int i = 0; i < count; i++)
        {
			Monster newMonster = Instantiate(monsterPrefab);
			stack_Monster.Push(newMonster);
			newMonster.transform.SetParent(monsterPoolParent.transform);
			newMonster.gameObject.SetActive(false);
        }
    }

	private void MakeBulletPool(int count)
	{
		Bullet bulletPrefab = ResourceManager.instance.GetMonoBehavioursObject<Bullet>("Bullet");

        for (int i = 0; i < count; i++)
		{
			Bullet newBullet = Instantiate(bulletPrefab);
			stack_Bullet.Push(newBullet);
			newBullet.transform.SetParent(bulletPoolParent.transform);
			newBullet.gameObject.SetActive(false);
		}
	}
}