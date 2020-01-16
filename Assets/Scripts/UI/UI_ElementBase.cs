using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ElementBase : MonoBehaviour
{
	public GameObject gameObjectCached { get; private set; }
	public Transform transformCached { get; private set; }

	public void SetActive(bool show)
	{
		if (gameObjectCached == null)
			gameObjectCached = gameObject;

		gameObjectCached.SetActive(show);
	}

	protected virtual void OnClickButtons(string buttonName) { }

	protected virtual void Awake()
	{
		gameObjectCached = gameObject;
		transformCached = transform;

		string buttonNamePrefix = "Button_";

		Button[] buttons = GetComponentsInChildren<Button>(true);

		int len = buttons.Length;
		for (int i = 0; i < len; i++)
		{
			Button button = buttons[i];

			string buttonName = button.name;
			if (buttonName.StartsWith(buttonNamePrefix))
				button.onClick.AddListener(() => OnClickButtons(buttonName));
		}
	}

	protected virtual void Start()
	{

	}
}