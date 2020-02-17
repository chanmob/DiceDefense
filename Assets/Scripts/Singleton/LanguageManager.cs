/*
  ============================================
	Author	: KJH
	Time 	: 2019-12-24 오후 2:32:56
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : Singleton<LanguageManager>
{
	/* [PUBLIC VARIABLE]					*/

	public event System.Action onLangaugeChanged;

	/* [PROTECTED && PRIVATE VARIABLE]		*/

	private readonly string[] ResourcePaths = new string[] {
		"DataTables/LanguageTable",
	};

	private const string Key = "Key";

	[SerializeField] private Transform[] _findTransforms;

	private Dictionary<string, string> _currentLangData = new Dictionary<string, string>();

	private Dictionary<string, List<LanguageComponent>> _langComponents = new Dictionary<string, List<LanguageComponent>>();


	/*----------------[PUBLIC METHOD]------------------------------*/

	public void RegisterComponent(LanguageComponent langComponent)
	{
		string key = langComponent.key;
		if (_langComponents.ContainsKey(key) == false)
			_langComponents.Add(langComponent.key, new List<LanguageComponent>());

		_langComponents[langComponent.key].Add(langComponent);
	}

	public void SetLangauge(SystemLanguage lang)
	{
		_currentLangData = GetLanguageData(lang.ToString());

		var iter = _langComponents.GetEnumerator();
		while (iter.MoveNext())
		{
			List<LanguageComponent> langComponents = iter.Current.Value;
			int count = langComponents.Count;
			for (int i = 0; i < count; i++)
			{
				LanguageComponent compo = langComponents[i];
				string value = GetCurrentLanguageText(compo.key);

				compo.SetText(value);
			}
		}

		onLangaugeChanged?.Invoke();
	}

	public string GetCurrentLanguageText(string key)
	{
		string value = string.Empty;
		if (_currentLangData.TryGetValue(key, out value) == false)
		{
			Debug.Log(key + " 키의 언어값이 없습니다.");
			return value;
		}

		return value;
	}

	public Dictionary<string, string> GetLanguageData(string lang)
	{
		Dictionary<string, string> dict = new Dictionary<string, string>();

		string merge = string.Empty;

		System.Text.StringBuilder builder = new System.Text.StringBuilder();
		int len = ResourcePaths.Length;
		for (int i = 0; i < ResourcePaths.Length; i++)
		{
			TextAsset textAsset = Resources.Load<TextAsset>(ResourcePaths[i]);

			string tsv = textAsset.text;
			if (string.IsNullOrEmpty(tsv)) return dict;

			builder.Append(tsv).Append("\n");
		}

		merge = builder.ToString();

		// 0 |	id,		Korean,		 English
		//		Text_Accept, 확인,		Accept

		List<Dictionary<string, object>> table = CTSVReader.Read(merge);
		if (table.Count == 0) return dict;

		int count = table.Count;
		for (int i = 0; i < count; i++)
		{
			Dictionary<string, object> values = table[i];

			if (values.ContainsKey(Key) == false)
			{
				continue;
			}
			if (values.ContainsKey(lang) == false)
			{
				continue;
			}

			string key = values[Key].ToString();
			string value = values[lang].ToString();

			if (dict.ContainsKey(key) == false)
				dict.Add(key, value);
		}

		return dict;
	}


	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	protected override void OnAwake()
	{
		base.OnAwake();

		Transform[] transforms = _findTransforms;

		int len = transforms.Length;
		for (int i = 0; i < len; i++)
		{
			Transform trans = transforms[i];

			LanguageComponent[] components = trans.GetComponentsInChildren<LanguageComponent>(true);
			int compoLen = components.Length;
			for (int j = 0; j < compoLen; j++)
			{
				LanguageComponent compo = components[j];
				RegisterComponent(compo);
			}
		}

		LoadLanguage();
	}

	private void LoadLanguage()
	{
		//SystemLanguage currentLanguage = GameDataManager.Instance.userInfo.language;
		//_currentLangData = GetLanguageData(currentLanguage.ToString());
		//SetLangauge(currentLanguage);
	}
}