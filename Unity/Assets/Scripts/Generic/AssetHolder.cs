using System;
using UnityEngine;

public class AssetHolder<T> : ScriptableObject where T : ScriptableObject
{
	private static T s_instance;

	public static T Instance
	{
		get
		{
			if (AssetHolder<T>.s_instance == null)
			{
				T[] array = Resources.FindObjectsOfTypeAll<T>();
				string name = typeof(T).Name;
				UnityEngine.Object @object;
				if (array.Length >= 1)
				{
					@object = array[0];
				}
				else
				{
					@object = Resources.Load(name);
				}
				AssetHolder<T>.s_instance = (T)((object)@object);
			}
			return AssetHolder<T>.s_instance;
		}
	}

	public void OnEnable()
	{
		AssetHolder<T>.s_instance = (this as T);
	}
}
