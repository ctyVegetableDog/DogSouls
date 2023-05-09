using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// �ṩ����ͬ�����첽����
/// ���ڼ��س����������Ƿ���ջ���أ��Ƿ�����¼��б���û��ã��о����Ե����ó�ȥ
/// </summary>
public class SceneMgr : Singleton<SceneMgr>
{
    /// <summary>
    /// ͨ��������ͬ�����س���
    /// </summary>
    /// <param name="name">Ҫ���صĳ�����</param>
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// ͨ���������ͬ������
    /// </summary>
    /// <param name="idx">�������</param>
    public void LoadScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    /// <summary>
    /// ͨ���������첽���أ����ڼ��ؽ�������лص����ڼ����в��ϸ��½�����
    /// </summary>
    /// <param name="name">������</param>
    /// <param name="callback">�ص�����</param>
    public void LoadSceneAysnc(string name, UnityAction callback)
    {
        MonoManager.GetInstance().StartCoroutine(LoadAysnc(name, callback));
        callback();
    }
    /// <summary>
    /// ͨ����������첽���أ����ڼ��ؽ�������лص����ڼ����в��ϸ��½�����
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="callback"></param>
    public void LoadSceneAysnc(int idx, UnityAction callback)
    {
        MonoManager.GetInstance().StartCoroutine(LoadAysnc(idx, callback));
        callback();
    }

    /// <summary>
    /// ͨ���������첽���أ������ص�
    /// </summary>
    /// <param name="name">������</param>
    /// <param name="callback">�ص�����</param>
    /// <returns></returns>
    private IEnumerator LoadAysnc(string name, UnityAction callback)
    {
        // �����첽����
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        // ����δ���ؽ�����һֱ���½���
        while (!operation.isDone)
        {
            // ���½��ȣ���UI�ﶩ��LoadingProgress����
            EventCenter.GetInstance().PublishEvent("LoadingProgress", operation.progress);
            yield return operation.progress;
        }
        callback();
        yield return operation;
    }

    /// <summary>
    /// ͨ����������첽���أ������ص�
    /// </summary>
    /// <param name="idx">�������</param>
    /// <param name="callback">�ص�����</param>
    /// <returns></returns>
    private IEnumerator LoadAysnc(int idx, UnityAction callback)
    {
        // �����첽����
        AsyncOperation operation = SceneManager.LoadSceneAsync(idx);
        // ����δ���ؽ�����һֱ���½���
        while (!operation.isDone)
        {
            // ���½��ȣ���UI�ﶩ��LoadingProgress����
            EventCenter.GetInstance().PublishEvent("LoadingProgress", operation.progress);
            yield return operation.progress;
        }
        callback();
        yield return operation;
    }
}