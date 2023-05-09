using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class MonoManager : Singleton<MonoManager>
{
    #region 实际的MonoController，提供update方法

    /// <summary>
    /// 给未继承MonoBehavior的类提供Update方法
    /// 使用委托接受其他类中的想在Update中执行的方法，然后在自己的Update里调用它们
    /// 使用MonoManager调用其单例
    /// </summary>
    private class MonoController : MonoBehaviour
    {
        // 在Update中调用
        private event UnityAction updateEvent;
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        private void Update()
        {
            if (updateEvent != null)
                updateEvent();
        }
        // 订阅
        public void Subscribe(UnityAction action)
        {
            updateEvent += action;
        }
        // 取消订阅
        public void Unsubscribe(UnityAction action)
        {
            updateEvent -= action;
        }
    }

    #endregion

    private MonoController monoController;
    public MonoManager()
    {
        GameObject obj = new GameObject();
        monoController = obj.AddComponent<MonoController>();
    }
    /// <summary>
    /// 订阅update事件
    /// </summary>
    /// <param name="action">订阅的事件</param>
    public void Subscribe(UnityAction action)
    {
        monoController.Subscribe(action);
    }
    /// <summary>
    /// 取消订阅
    /// </summary>
    /// <param name="action">取消订阅的事件</param>
    public void Unsubscribe(UnityAction action)
    {
        monoController.Unsubscribe(action);
    }
    /// <summary>
    /// 用函数名开启协程
    /// </summary>
    /// <param name="methodName">函数名</param>
    /// <param name="value">参数列表</param>
    /// <returns></returns>
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return monoController.StartCoroutine(methodName, value);
    }
    /// <summary>
    /// 用迭代器开启协程
    /// </summary>
    /// <param name="routine">迭代器</param>
    /// <returns></returns>
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return monoController.StartCoroutine(routine);
    }
    /// <summary>
    /// 用函数名开启协程
    /// </summary>
    /// <param name="methodName">函数名</param>
    /// <returns></returns>
    public Coroutine StartCoroutine(string methodName)
    {
        return monoController.StartCoroutine(methodName);
    }
}
