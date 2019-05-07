using System;
using System.Collections;
using UnityEngine;

public static class GameObjectExtensions
{
	public static void SetActiveIfNeeded(this GameObject go, bool active)
	{
		if (go.activeInHierarchy != active)
		{
			go.SetActive(active);
		}
	}

	public static void SetActiveIfNeededSelf(this GameObject go, bool active)
	{
		if (go.activeSelf != active)
		{
			go.SetActive(active);
		}
	}

	public static GameObject EnsureChild(this GameObject gameObject, string childName, Action<GameObject> createHandler = null)
	{
		Transform transform = gameObject.transform.Find(childName);
		if (transform != null)
		{
			return transform.gameObject;
		}
		GameObject gameObject2 = new GameObject();
		gameObject2.name = childName;
		gameObject2.transform.SetParent(gameObject.transform);
		gameObject2.transform.localPosition = Vector3.zero;
		gameObject2.transform.localScale = Vector3.one;
		if (createHandler != null)
		{
			createHandler(gameObject2);
		}
		return gameObject2;
	}

	public static Transform ClearChildren(this Transform transform)
	{
		IEnumerator enumerator = transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform2 = (Transform)enumerator.Current;
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		return transform;
	}
}
