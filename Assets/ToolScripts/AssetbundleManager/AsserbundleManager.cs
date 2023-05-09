using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// AB��������
/// </summary>

public class AssetbundleManager : Singleton<AssetbundleManager>
{
    // ����·��
    public string PathUrl { get { return StaticNameInfo.AssetbundlePathUrl; } }
    // ������
    public string MainABName { get { return StaticNameInfo.AssetbundleMainBundleName; } }
    // ����
    private AssetBundle mainAB = null;
    // �����еĹؼ�����
    private AssetBundleManifest manifest = null;
    // �洢�Ѿ����ع���AB�������ظ�����
    Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// ���ص����������ð�δ�����أ���������������������
    /// </summary>
    /// <param name="targetBundleName">����ص�AB����</param>
    public void LoadSingleAssetBundle(string targetBundleName)
    {
        // ��������
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            // ���������еĹؼ������ļ����̶�д��
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        // �鿴��Ҫ���ذ�������
        string[] str = manifest.GetAllDependencies(targetBundleName);
        // ����������
        for (int i = 0; i < str.Length; ++i)
        {
            // �ֱ����ÿ��������
            if (!abDic.ContainsKey(str[i]))
            {
                // ���ش˰���Ȼ��������ֵ�
                AssetBundle assetBundle = AssetBundle.LoadFromFile(PathUrl + str[i]);
                abDic.Add(str[i], assetBundle);
            }
        }
        // ����Ŀ���
        if (!abDic.ContainsKey(targetBundleName))
        {
            // ���ش˰���Ȼ��������ֵ�
            AssetBundle assetBundle = AssetBundle.LoadFromFile(PathUrl + targetBundleName);
            abDic.Add(targetBundleName, assetBundle);
        }
    }

    /// <summary>
    /// ͬ��������Դ����ָ�����ͣ������غõ���Դʵ����������
    /// </summary>
    /// <param name="targetBundleName">Ŀ��AssetBundle��</param>
    /// <param name="resourceName">������Դ��</param>
    /// <returns>���س�����Դ</returns>
    public Object LoadRes(string targetBundleName, string resourceName)
    {
        // ����Ŀ���
        LoadSingleAssetBundle(targetBundleName);
        // ������Դ
        AssetBundle targetBundle = abDic[targetBundleName];
        Object res = targetBundle.LoadAsset(resourceName);
        return GameObject.Instantiate(res);
    }

    /// <summary>
    /// ͬ��������Դ��ָ�����ͣ������غõ���Դʵ����������
    /// </summary>
    /// <param name="targetBundleName">Ŀ��AB����</param>
    /// <param name="resourceName">Ŀ����Դ��</param>
    /// <param name="type">��Դ����</param>
    /// <returns>���س�����Դ</returns>
    public Object LoadRes(string targetBundleName, string resourceName, System.Type type)
    {
        // ����Ŀ���
        LoadSingleAssetBundle(targetBundleName);
        // ������Դ
        AssetBundle targetBundle = abDic[targetBundleName];
        Object res = targetBundle.LoadAsset(resourceName, type);
        return GameObject.Instantiate(res);
    }

    /// <summary>
    /// ͬ��������Դ�����ͣ������غõ���Դʵ����������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetBundleName">Ŀ��AB����</param>
    /// <param name="resourceName">Ŀ����Դ��</param>
    /// <returns>���س���AB��Դ</returns>
    public T LoadRes<T>(string targetBundleName, string resourceName) where T : Object
    {
        // ����Ŀ���
        LoadSingleAssetBundle(targetBundleName);
        // ������Դ
        AssetBundle targetBundle = abDic[targetBundleName];
        T res = targetBundle.LoadAsset<T>(resourceName);
        return GameObject.Instantiate(res);
    }

    /// <summary>
    /// �첽������Դ����ָ�����͡�AB����Ȼ��ͬ�����أ���Դ�첽���أ���������ڻص������д������
    /// </summary>
    /// <param name="targetBundleName">Ŀ��������ð�ͬ������</param>
    /// <param name="resourceName">Ŀ����Դ�����첽����</param>
    /// <param name="callback">�ص�����</param>
    public void LoadResAsync(string targetBundleName, string resourceName, UnityAction<Object> callback)
    {
        // ����ӿ�ֻ��������Э��
        MonoManager.GetInstance().StartCoroutine(LoadResAsyncHelper(targetBundleName, resourceName, callback));
    }
    /// <summary>
    /// ʹ��Э�̼�����Դ�����ڻص�������ʹ�ã������غõ���Դʵ����������
    /// </summary>
    /// <param name="targetBundleName">Ŀ�����</param>
    /// <param name="resourceName">Ŀ����Դ��</param>
    /// <param name="callback">�ص�����</param>
    /// <returns></returns>
    private IEnumerator LoadResAsyncHelper(string targetBundleName, string resourceName, UnityAction<Object> callback)
    {

        // ����Ŀ���
        LoadSingleAssetBundle(targetBundleName);
        // ������Դ
        AssetBundle targetBundle = abDic[targetBundleName];
        AssetBundleRequest res = targetBundle.LoadAssetAsync(resourceName);
        yield return res;
        // �ص�������ͨ��ί�д��ݸ��ⲿ���ⲿ��ʹ��
        callback(GameObject.Instantiate(res.asset));
        yield return null;
    }

    /// <summary>
    /// �첽������Դ��ָ�����͡�AB����Ȼ��ͬ�����أ���Դ�첽���أ���������ڻص������д�����󣬽����غõĶ���ʵ����������
    /// </summary>
    /// <param name="targetBundleName">Ŀ��������ð�ͬ������</param>
    /// <param name="resourceName">Ŀ����Դ�����첽����</param>
    /// <param name="type">Ŀ����Դ����</param>
    /// <param name="callback">�ص�����</param>
    public void LoadResAsync(string targetBundleName, string resourceName, System.Type type, UnityAction<Object> callback)
    {
        // ����ӿ�ֻ��������Э��
        MonoManager.GetInstance().StartCoroutine(LoadResAsyncHelper(targetBundleName, resourceName, type, callback));
    }
    /// <summary>
    /// ʹ��Э�̼�����Դ�����ڻص�������ʹ��
    /// </summary>
    /// <param name="targetBundleName">Ŀ�����</param>
    /// <param name="resourceName">Ŀ����Դ��</param>
    /// <param name="type">Ŀ����Դ����</param>
    /// <param name="callback">�ص�����</param>
    /// <returns></returns>
    private IEnumerator LoadResAsyncHelper(string targetBundleName, string resourceName, System.Type type, UnityAction<Object> callback)
    {

        // ����Ŀ���
        LoadSingleAssetBundle(targetBundleName);
        // ������Դ
        AssetBundle targetBundle = abDic[targetBundleName];
        AssetBundleRequest res = targetBundle.LoadAssetAsync(resourceName, type);
        yield return res;
        // �ص�������ͨ��ί�д��ݸ��ⲿ���ⲿ��ʹ��
        callback(GameObject.Instantiate(res.asset));
        yield return null;
    }

    /// <summary>
    /// �첽������Դ��ʹ�÷��͡�AB����Ȼ��ͬ�����أ���Դ�첽���أ���������ڻص������д�����󣬽����غõĶ���ʵ����������
    /// </summary>
    /// <param name="targetBundleName">Ŀ��������ð�ͬ������</param>
    /// <param name="resourceName">Ŀ����Դ�����첽����</param>
    /// <param name="callback">�ص�����</param>
    public void LoadResAsync<T>(string targetBundleName, string resourceName, UnityAction<T> callback) where T : Object
    {
        // ����ӿ�ֻ��������Э��
        MonoManager.GetInstance().StartCoroutine(LoadResAsyncHelper<T>(targetBundleName, resourceName, callback));
    }
    /// <summary>
    /// ʹ��Э�̼�����Դ��ʹ�÷��ͣ����ڻص�������ʹ��
    /// </summary>
    /// <param name="targetBundleName">Ŀ�����</param>
    /// <param name="resourceName">Ŀ����Դ��</param>
    /// <param name="callback">�ص�����</param>
    /// <returns></returns>
    private IEnumerator LoadResAsyncHelper<T>(string targetBundleName, string resourceName, UnityAction<T> callback) where T : Object
    {

        // ����Ŀ���
        LoadSingleAssetBundle(targetBundleName);
        // ������Դ
        AssetBundle targetBundle = abDic[targetBundleName];
        AssetBundleRequest res = targetBundle.LoadAssetAsync<T>(resourceName);
        yield return res;
        // �ص�������ͨ��ί�д��ݸ��ⲿ���ⲿ��ʹ��
        callback(GameObject.Instantiate(res.asset) as T);
        yield return null;
    }


    /// <summary>
    /// ж�ص����������ð���������ж�أ�����ʲôҲ����
    /// </summary>
    /// <param name="targetBundleName"></param>
    public void Unload(string targetBundleName)
    {
        //���ð��ѱ����ز���ж��
        if (abDic.ContainsKey(targetBundleName))
        {
            // ж�ظð��ұ�����������Դ
            abDic[targetBundleName].Unload(false);
            // ���ð����ֵ����Ƴ�
            abDic.Remove(targetBundleName);
        }
    }
    /// <summary>
    /// ж�������Ѽ��ص�AB��
    /// </summary>
    public void UnloadAll()
    {
        // ж�����а�
        AssetBundle.UnloadAllAssetBundles(false);
        // ����ֵ�
        abDic.Clear();
    }
}