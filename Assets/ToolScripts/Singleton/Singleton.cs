using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 没有MONOBEHAVIOR的单例
/// 让某个类继承自该类使其变为单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T:new()
{
    private static T instance;
    protected Singleton() { }
    public static T GetInstance()
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }
}
