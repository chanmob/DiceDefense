/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-02-17 오후 1:32:07
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageComponent : MonoBehaviour
{
	/* [PUBLIC VARIABLE]					*/

	public string key;

	/* [PROTECTED && PRIVATE VARIABLE]		*/

	private Text _textComponent;

	private bool _isAwaked;

	/*----------------[PUBLIC METHOD]------------------------------*/

	public void SetText(string text)
	{
		Awake();
		_textComponent.text = text;
	}

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	private void Awake()
	{
		if (_isAwaked) return;

		_textComponent = GetComponent<Text>();

		if (string.IsNullOrEmpty(key))
			key = name;

		_isAwaked = true;
	}
}