using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ʹ��Monobehavior�ĵ���
/// ���Ƽ�ʹ��
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T instance;
    public static T GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject();
            obj.name = typeof(T).ToString();
            instance = obj.AddComponent<T>();
            DontDestroyOnLoad(obj);
        }
        return instance;
    }
}
