/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-14 오후 10:38:29
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : Singleton<SceneControl>
{
    /* [PUBLIC VARIABLE]					*/


    /* [PROTECTED && PRIVATE VARIABLE]		*/


    /*----------------[PUBLIC METHOD]------------------------------*/

    public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name);
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/
}