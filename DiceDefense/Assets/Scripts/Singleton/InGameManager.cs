/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-14 오후 11:37:32
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    /* [PUBLIC VARIABLE]					*/

    public Unit unit;

	public List<Monster> monsterList;

	[HideInInspector] public int round;
	[HideInInspector] public int gold;

    public int[] percent = new int[3];

    public int amount_Upgrade1 = 0;
    public int amount_Upgrade2 = 0;
    public int amount_Upgrade3 = 0;

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    [SerializeField] private Transform backgroundParent;

	private List<int> spawnIndex;

	private List<Transform> spawnTransform;

	private bool[] isSpawned;

	/*----------------[PUBLIC METHOD]------------------------------*/

	public void BuyUnit()
	{
		int index = RandomSpawnIndex(spawnIndex);

        int rank = GetPercent();
        int division = Random.Range(0, 3);

        Instantiate(UnitManager.instance.units[division], spawnTransform[index].position, Quaternion.identity);

        //switch (rank)
        //{
        //    case 0:
        //        Instantiate(UnitManager.instance.units[division], spawnTransform[index].position, Quaternion.identity);
        //        break;
        //    case 1:
        //        Instantiate(UnitManager.instance.units[division], spawnTransform[index].position, Quaternion.identity);
        //        break;
        //    case 2:
        //        Instantiate(UnitManager.instance.hiddenUnits[division], spawnTransform[index].position, Quaternion.identity);
        //        break;
        //}

        isSpawned[index] = true;
	}

	public void GetGold(int value)
	{
		gold += value;
		InGameUIManager.instance.panel_MainInGame.text_gold.text = gold.ToString();
	}

    public bool CheckGold(int cost)
    {
        return cost <= gold;
    }

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	protected override void Awake()
	{
		base.Awake();

		monsterList = new List<Monster>();
		spawnIndex = new List<int>();
		spawnTransform = new List<Transform>();

		Transform[] backgroundChildren = backgroundParent.GetComponentsInChildren<Transform>();
		int len = backgroundChildren.Length;
		isSpawned = new bool[len];

		for (int i = 1; i < len; i++)
		{
			spawnTransform.Add(backgroundChildren[i]);
			spawnIndex.Add(i - 1);
		}
	}

	private int RandomSpawnIndex(List<int> list_int)
	{
		int idx = Random.Range(0, list_int.Count);
		int returnValue = 0;

		returnValue = list_int[idx];
		list_int.Remove(list_int[idx]);

		return returnValue;
	}

    /// <summary>
    /// 0 - 노말, 1 - 슈퍼, 2- 히든
    /// </summary>
    /// <returns></returns>
    private int GetPercent()
    {
        int len = percent.Length;
        int sum = 0;
        int returnValue = 0;

        for(int i = 0; i < len; i++)
        {
            sum += percent[i];
        }

        int ran = Random.Range(0, sum);

        for(int i = 0; i < len; i++)
        {
            ran -= percent[i];

            if(ran <= 0)
            {
                returnValue = i;
            }
        }

        return returnValue;
    }

	//private void Update()
	//   {
	//       if (Input.GetMouseButtonDown(0))
	//       {
	//           Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	//           RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity);

	//           if (hit)
	//           {
	//               Debug.Log(hit.collider.name + "Hit");

	//               if (hit.collider.CompareTag("Board") && unit != null)
	//               {
	//                   unit.MoveToClickPosition(pos);
	//                   unit = null;
	//                   Debug.Log("This Is Board!");

	//               }
	//               else if (hit.collider.CompareTag("Unit"))
	//               {
	//                   unit = hit.collider.GetComponent<Unit>();
	//                   Debug.Log("This Is Unit!");
	//               }
	//           }
	//           else
	//           {
	//               Debug.Log("Not Hit");
	//           }
	//       }
	//   }
}