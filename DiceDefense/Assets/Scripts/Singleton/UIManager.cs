/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-15 오후 5:46:35
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	/* [PUBLIC VARIABLE]					*/


	/* [PROTECTED && PRIVATE VARIABLE]		*/

	private Dictionary<string, UI_Base> _uiInstances = new Dictionary<string, UI_Base>();

	/*----------------[PUBLIC METHOD]------------------------------*/


	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	protected override void Awake()
	{
		base.Awake();

		SetUIInstance();
	}

	private void SetUIInstance()
	{
		UI_Base[] uis = GetComponentsInChildren<UI_Base>(true);

		int len = uis.Length;
		for (int i = 0; i < len; i++)
		{
			UI_Base ui = uis[i];
			string uiName = ui.name;

			_uiInstances.Add(uiName.ToUpper(), ui);
			ui.gameObject.SetActive(true);
		}
	}

	protected bool GetUiInstance<T>(out T uiInstance)
	where T : UI_Base
	{
		uiInstance = null;

		UI_Base ui;
		if (_uiInstances.TryGetValue(typeof(T).Name.ToUpper(), out ui))
			uiInstance = (ui as T);

		return (uiInstance != null);
	}
}