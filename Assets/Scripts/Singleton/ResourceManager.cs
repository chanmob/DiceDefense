/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-01-14 오후 10:49:01
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
	/* [PUBLIC VARIABLE]					*/

	public const string PathFormat = "{0}/{1}";

	/* [PROTECTED && PRIVATE VARIABLE]		*/

	private Dictionary<string, List<string>> _resourcesPath = new Dictionary<string, List<string>>();

	private Dictionary<string, Object> _objects = new Dictionary<string, Object>();
	private Dictionary<string, MonoBehaviour> _monoBehaviours = new Dictionary<string, MonoBehaviour>();

	/*----------------[PUBLIC METHOD]------------------------------*/

	public T GetObject<T>(string prefabName)
where T : Object
	{
		Object resource;
		if (_objects.TryGetValue(prefabName, out resource) == false)
			Debug.Log(prefabName + " 리소스가 존재하지 않습니다.");

		return (resource as T);
	}

	public T GetMonoBehavioursObject<T>(string prefabName)
		where T : MonoBehaviour
	{
		MonoBehaviour resource;

		if (_monoBehaviours.TryGetValue(prefabName, out resource) == false)
			Debug.Log(prefabName + " 리소스가 존재하지 않습니다.");

		return (resource as T);
	}

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/

	private void InitLoadResourcesPath(string path, DataDefine.ResourceType resourceType)
	{
		Object[] objects = Resources.LoadAll(path);

		int len = objects.Length;
		for (int i = 0; i < len; i++)
		{
			Object obj = objects[i];
			string prefabName = obj.name;

			switch (resourceType)
			{
				case DataDefine.ResourceType.Resource:
					LoadObject(path, prefabName);
					break;
				case DataDefine.ResourceType.MonoBehaviour:
					LoadMonoBehaviour(path, prefabName);
					break;
				case DataDefine.ResourceType.Sprite:
					LoadSprite(path, prefabName);
					break;
			}
		}

		Debug.Log("ResourcesManager LoadResource " + path + ", Count: " + len);
	}


	private void LoadObject(string path, string prefabName)
	{
		if (_resourcesPath.ContainsKey(path) == false)
			_resourcesPath.Add(path, new List<string>());

		if (_objects.ContainsKey(prefabName)) return;

		Object prefab = Resources.Load(string.Format(PathFormat, path, prefabName));
		if (prefab != null)
		{
			_objects.Add(prefabName, prefab);
			_resourcesPath[path].Add(prefabName);
		}
	}

	private void LoadMonoBehaviour(string path, string prefabName)
	{
		if (_resourcesPath.ContainsKey(path) == false)
			_resourcesPath.Add(path, new List<string>());

		if (_monoBehaviours.ContainsKey(prefabName)) return;

		MonoBehaviour prefab = Resources.Load<MonoBehaviour>(string.Format(PathFormat, path, prefabName));
		if (prefab != null)
		{
			_monoBehaviours.Add(prefabName, prefab);
			_resourcesPath[path].Add(prefabName);
		}
        else
        {
            Debug.LogError(prefabName + "MonoBehaviour Prefab 존재하지 않음");
        }
	}

	private void LoadSprite(string path, string prefabName)
	{
		if (_resourcesPath.ContainsKey(path) == false)
			_resourcesPath.Add(path, new List<string>());

		if (_objects.ContainsKey(prefabName)) return;

		Sprite prefab = Resources.Load<Sprite>(string.Format(PathFormat, path, prefabName));
		if (prefab != null)
		{
			_objects.Add(prefabName, prefab);
			_resourcesPath[path].Add(prefabName);
		}
	}

	protected override void Awake()
	{
		base.Awake();

        InitLoadResourcesPath("Sprites", DataDefine.ResourceType.Sprite);
		InitLoadResourcesPath("Monster", DataDefine.ResourceType.MonoBehaviour);
		InitLoadResourcesPath("Bullet", DataDefine.ResourceType.MonoBehaviour);
	}
}