using System;
using UnityEngine;


/// <summary>
/// 单例基类，自动继承mono
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMonoBase<T> : MonoBehaviour where T : MonoBehaviour
{

    protected SingletonMonoBase() { }

    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }
}



/// <summary>
/// 不自动继承mono
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonBase<T> where T : SingletonBase<T>
{
    protected SingletonBase() { }
    private static object locker = new object();
    private volatile static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = Activator.CreateInstance(typeof(T), true) as T;
                    }
                }
            }
            return instance;
        }
    }
}

