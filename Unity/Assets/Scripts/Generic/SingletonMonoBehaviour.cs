using System;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T s_instance;

	private static bool s_shuttingDown;

	public static T Instance
	{
		get
		{
			if (SingletonMonoBehaviour<T>.s_shuttingDown)
			{
				return (T)((object)null);
			}
			if (SingletonMonoBehaviour<T>.s_instance == null)
			{
				SingletonMonoBehaviour<T>.s_instance = UnityEngine.Object.FindObjectOfType<T>();
			}
			if (SingletonMonoBehaviour<T>.s_instance == null)
			{
				GameObject gameObject = new GameObject();
				SingletonMonoBehaviour<T>.s_instance = gameObject.AddComponent<T>();
				gameObject.name = string.Format("{0} (singleton)", typeof(T).Name);
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			return SingletonMonoBehaviour<T>.s_instance;
		}
		protected set
		{
			if (SingletonMonoBehaviour<T>.s_instance != null && SingletonMonoBehaviour<T>.s_instance != value)
			{
				UnityEngine.Object.Destroy(SingletonMonoBehaviour<T>.s_instance.gameObject);
			}
			SingletonMonoBehaviour<T>.s_instance = value;
			UnityEngine.Object.DontDestroyOnLoad(SingletonMonoBehaviour<T>.s_instance.gameObject);
		}
	}

	private void OnDestroy()
	{
		if (SingletonMonoBehaviour<T>.s_instance == this)
		{
			SingletonMonoBehaviour<T>.s_shuttingDown = true;
		}
	}
}
