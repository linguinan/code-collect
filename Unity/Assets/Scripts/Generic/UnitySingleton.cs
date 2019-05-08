/*
 * @Author: lgn 
 * @Date: 2018-02-05 23:19:51 
 * @Last Modified by: lgn
 * @Last Modified time: 2019-05-08 00:20:39
 */
using UnityEngine;

/** 
* Unity单例对象·GameObject实例
* 用法：class Type : UnitySingleton<Type>
*/
public abstract class UnitySingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    private static bool _shuttingDown;

    public static T Instance
    {
        get
        {
            if (_shuttingDown)
			{
				return (T)((object)null);
			}
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    var obj = new GameObject("[--- " + typeof(T).Name + " ---]");
                    _instance = (T)obj.AddComponent(typeof(T));
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnDestroy()  
    {
        if (_instance == this)
		{
			_shuttingDown = true;
		}
    }
}