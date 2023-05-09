using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 提供场景同步，异步加载
/// 关于加载场景结束后是否清空缓存池，是否清空事件列表，还没想好，感觉可以单独拿出去
/// </summary>
public class SceneMgr : Singleton<SceneMgr>
{
    /// <summary>
    /// 通过场景名同步加载场景
    /// </summary>
    /// <param name="name">要加载的场景名</param>
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// 通过场景编号同步加载
    /// </summary>
    /// <param name="idx">场景编号</param>
    public void LoadScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    /// <summary>
    /// 通过场景名异步加载，并在加载结束后进行回调，在加载中不断更新进度条
    /// </summary>
    /// <param name="name">场景名</param>
    /// <param name="callback">回调函数</param>
    public void LoadSceneAysnc(string name, UnityAction callback)
    {
        MonoManager.GetInstance().StartCoroutine(LoadAysnc(name, callback));
        callback();
    }
    /// <summary>
    /// 通过场景编号异步加载，并在加载结束后进行回调，在加载中不断更新进度条
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="callback"></param>
    public void LoadSceneAysnc(int idx, UnityAction callback)
    {
        MonoManager.GetInstance().StartCoroutine(LoadAysnc(idx, callback));
        callback();
    }

    /// <summary>
    /// 通过场景名异步加载，并做回调
    /// </summary>
    /// <param name="name">场景名</param>
    /// <param name="callback">回调函数</param>
    /// <returns></returns>
    private IEnumerator LoadAysnc(string name, UnityAction callback)
    {
        // 场景异步加载
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        // 场景未加载结束就一直更新进度
        while (!operation.isDone)
        {
            // 更新进度，在UI里订阅LoadingProgress就行
            EventCenter.GetInstance().PublishEvent("LoadingProgress", operation.progress);
            yield return operation.progress;
        }
        callback();
        yield return operation;
    }

    /// <summary>
    /// 通过场景编号异步加载，并做回调
    /// </summary>
    /// <param name="idx">场景编号</param>
    /// <param name="callback">回调函数</param>
    /// <returns></returns>
    private IEnumerator LoadAysnc(int idx, UnityAction callback)
    {
        // 场景异步加载
        AsyncOperation operation = SceneManager.LoadSceneAsync(idx);
        // 场景未加载结束就一直更新进度
        while (!operation.isDone)
        {
            // 更新进度，在UI里订阅LoadingProgress就行
            EventCenter.GetInstance().PublishEvent("LoadingProgress", operation.progress);
            yield return operation.progress;
        }
        callback();
        yield return operation;
    }
}