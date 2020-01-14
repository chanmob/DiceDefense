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

    /* [PROTECTED && PRIVATE VARIABLE]		*/


    /*----------------[PUBLIC METHOD]------------------------------*/


    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity);

            if (hit)
            {
                Debug.Log(hit.collider.name + "Hit");

                if (hit.collider.CompareTag("Board") && unit != null)
                {
                    unit.MoveToClickPosition(pos);
                    unit = null;
                    Debug.Log("This Is Board!");

                }
                else if (hit.collider.CompareTag("Unit"))
                {
                    unit = hit.collider.GetComponent<Unit>();
                    Debug.Log("This Is Unit!");
                }
            }
            else
            {
                Debug.Log("Not Hit");
            }
        }
    }
}