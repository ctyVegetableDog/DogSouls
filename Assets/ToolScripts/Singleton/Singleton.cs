using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// û��MONOBEHAVIOR�ĵ���
/// ��ĳ����̳��Ը���ʹ���Ϊ����
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
